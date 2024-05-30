namespace Project_B;

using Newtonsoft.Json;

public static class Json_writer
{
    public static void WriteFilmToJSON(Film film, string fileName = "Films_test.json")
    {
        StreamWriter writer = new(fileName);

        List<Film> _films = new() { film };
        var json = JsonConvert.SerializeObject(_films.ToArray());
        writer.WriteLine(json);
        writer.Close();
    }

    public static void WriteFilmToJSON(List<Film> films, string fileName = "Films_test.json")
    {
        // overload for writing a list of films
        StreamWriter writer = new(fileName);

        var json = JsonConvert.SerializeObject(films);
        writer.WriteLine(json);
        writer.Close();
    }
    public static double WriteReservationToJSON(Film film, Zaal zaal, string datum, int age, string fileName = "Reservations.json", List<int> ChosenSeats = null, string email = "NoEmail")
    {
        // func that wirtes a reservation to json / makes user choose a film / processes the payment
        List<ReservationJsonObj> reservations = new() { };
        ReservationJsonObj existringReservation = null!;
        List<int> unavailavble_seats = new();
        bool emptyFile = false;
        double totalPrice = -1;

        if (ChosenSeats == null)
        {
            if (email == "NoEmail")
            {
                Console.WriteLine("Je bent niet ingelogd");
                Console.WriteLine("Druk op (Enter) en vul je email in, of druk op (esc) en Log in");
                ConsoleKeyInfo keyInfo;

                while (true)
                {
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        return -1;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine("Vul nu je email in:");
                        email = Console.ReadLine();

                        if (email != null && email != "")
                        {
                            if (AddAccount.ParseEmail(email))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Email moet in example@gmail.com formaat zijn");
                                Console.WriteLine("\nDruk Enter en probeer het opnieuw");
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
            if (age == 999)
            {
                Console.WriteLine("Je bent niet ingelogd");
                Console.WriteLine("Druk op (Enter) en vul je leeftijd in, of Druk op (esc) en Log in");
                ConsoleKeyInfo keyInfo;

                while (true)
                {
                    keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        return -1;
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine("Vul nu je leeftijd in:");
                        try
                        {
                            int ageAttempt = Convert.ToInt32(Console.ReadLine());
                            if (ageAttempt < 0 || ageAttempt > 111)
                            {
                                string message = ageAttempt < 0 ? "Leeftijd kan niet negatief zijn" : "Leeftijd kan niet boven 111 zijn";
                                Console.WriteLine(message);
                                Console.WriteLine("\nDruk Enter en probeer het opnieuw");
                                Console.ReadLine();


                            }
                            else
                            {
                                // valid age
                                age = ageAttempt;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Leeftijd moet een nummer zijn");
                            Console.WriteLine("\nDruk Enter en probeer het opnieuw");
                            Console.ReadLine();


                        }
                    }
                }
            }
        }




        // first return unavailble seats list
        if (!File.Exists(fileName))
        {
            emptyFile = true;
        }
        else if (File.ReadAllText(fileName) == "")
        {
            emptyFile = true;
        }
        else
        {
            existringReservation = existingrservationfinder(fileName, film, datum, zaal);
            if (existringReservation != null)
            { unavailavble_seats = new List<int>(existringReservation.Seats); }

        }
        // now select your seat
        // skips this step if ChosenSeat is null this for unit testing
        if (ChosenSeats == null)
        {
            ChosenSeats = SeatSelection.SelectSeat(zaal.Size, unavailavble_seats);
            // retruns null if you press escape in seat selection (goes back to main menu)
            if (ChosenSeats == null)
            { return -1; }

            // pay for your seat
            double price = SeatSaleRoom.GetMoviePrice(film.Name);
            double seatFee = 0;
            double ageFee = 0;
            int luxurySeatCount = 0;
            foreach (int seat in ChosenSeats)
            {
                seatFee += SeatSaleRoom.IsExpensive(zaal.Size, seat) ? price * 0.1 : 0; // 10% etra for expensive seat
                luxurySeatCount += SeatSaleRoom.IsExpensive(zaal.Size, seat) ? 1 : 0;

                ageFee = age < 18 ? price * 0.2 : 0; // 20% extra for under 18 customer
            }
            totalPrice = price * ChosenSeats.Count() + seatFee + ageFee;
            // if customer doesnt pay return false
            if (!Reservation.PrintPrice(price, seatFee, ageFee, ChosenSeats.Count(), luxurySeatCount))
            { return -1; }
        }

        // call write funcs for each seat seperately
        foreach (int seat in ChosenSeats)
        {
            // write reservation to json
            if (emptyFile)
            {
                // write new file / write to empty file
                WriteReservationToEmptyFile(zaal, datum, film, fileName, seat, email);
            }
            else if (existringReservation != null)
            {
                // add seat and email to reservation and write the updated list
                existringReservation.Seats.Add(seat);
                existringReservation.Emails.Add(email);

                string readJson = File.ReadAllText(fileName);
                reservations = JsonConvert.DeserializeObject<List<ReservationJsonObj>>(readJson)!;

                for (int i = 0; i < reservations.Count; i++)
                {
                    if (reservations[i].ID == existringReservation.ID)
                    {
                        reservations[i] = existringReservation;
                        break; // Exit the loop once the update is done
                    }
                }

                var json = JsonConvert.SerializeObject(reservations);
                WriteReservationsToJSON(reservations, fileName);
            }
            else
            {
                // add new reservation to file
                string readJson = File.ReadAllText(fileName);
                reservations = JsonConvert.DeserializeObject<List<ReservationJsonObj>>(readJson)!;
                AddNewReservationToFile(reservations, film, datum, zaal, fileName, seat, email);
            }
            // after writing the first seat update the existringReservation and set emtyfile bool to false
            emptyFile = false;
            existringReservation = existingrservationfinder(fileName, film, datum, zaal);
        }
        StatisticsManager.AddPaymentDatapoint(totalPrice, film, ChosenSeats.Count(), datum, age);
        return totalPrice;

    }

    private static bool WriteReservationToEmptyFile(Zaal zaal, string datum, Film film, string fileName, int ChosenSeat, string email = "NoEmail")
    {
        ReservationJsonObj reservation = new()
        {
            ID = $"{film.Name}|{film.Director}|{datum}|{zaal.ID}",
            Date = datum,
            ZaalID = zaal.ID,
            Seats = new List<int> { ChosenSeat },
            Emails = new List<string> { email }
        };

        StreamWriter writer = new(fileName);
        List<ReservationJsonObj> list_of_reservations = new() { reservation };
        var json = JsonConvert.SerializeObject(list_of_reservations);
        writer.WriteLine(json);
        writer.Close();

        // returns true if reservation was succesfull
        return true;
    }

    private static bool AddNewReservationToFile(List<ReservationJsonObj> reservations, Film film,
     string datum, Zaal zaal, string fileName, int ChosenSeat, string email = "NoEmail")
    {
        ReservationJsonObj reservation = new()
        {
            ID = $"{film.Name}|{film.Director}|{datum}|{zaal.ID}",
            Date = datum,
            ZaalID = zaal.ID,
            Seats = new List<int> { ChosenSeat },
            Emails = new List<string> { email }
        };
        reservations.Add(reservation);
        StreamWriter writer = new(fileName);
        var json = JsonConvert.SerializeObject(reservations);
        writer.WriteLine(json);
        writer.Close();

        // return true if reservation succesfull
        return true;

    }

    public static bool WriteReservationsToJSON(List<ReservationJsonObj> reservations, string fileName = "reservations.josn")
    {
        string json = JsonConvert.SerializeObject(reservations);
        File.WriteAllText(fileName, json);
        return true;
    }


    public class ReservationJsonObj
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public int ZaalID { get; set; }
        public List<int> Seats { get; set; } // This will be serialized as an array in JSON
        public List<string> Emails { get; set; }
    }



    public static void WriteZaal(Zaal zaal)
    {
        string fileName = "Zaalen.json";
        List<Zaal> Zalen = JsonReader.ReadZalen();
        if (Zalen == null)
        {
            WriteZaalToEmptyFile(fileName, zaal);
        }
        else
        {
            Zalen.Add(zaal);
            StreamWriter writer = new(fileName);
            var json = JsonConvert.SerializeObject(Zalen);

            writer.WriteLine(json);
            writer.Close();
        }
    }


    private static void WriteZaalToEmptyFile(string fileName, Zaal zaal)
    {
        StreamWriter writer = new(fileName);
        var json = JsonConvert.SerializeObject(new List<Zaal> { zaal });


        writer.WriteLine(json);
        writer.Close();
    }

    private static ReservationJsonObj existingrservationfinder(string fileName, Film film, string datum, Zaal zaal)
    {
        // read file
        string readJson = File.ReadAllText(fileName);
        // make list of reservation objects
        var reservations = JsonConvert.DeserializeObject<List<ReservationJsonObj>>(readJson)!;
        // look for reservation with same id
        foreach (ReservationJsonObj reservation in reservations!)
        {
            if ($"{film.Name}|{film.Director}|{datum}|{zaal.ID}" == reservation.ID)
            {
                return reservation;
            }
        }
        return null;
    }
}






