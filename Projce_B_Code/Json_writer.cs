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

    public static bool WriteReservationToJSON(Film film, Zaal zaal, string datum, string fileName = "Reservations.json", int ChosenSeat = -1, string email = "NoEmail")
    {
        List<int> unavailavble_seats = new();

        if (!File.Exists(fileName))
        {
            // file doest exists | make new file write reservation to new file
            bool reservationCompleterd = WriteReservationToEmptyFile(zaal, unavailavble_seats, datum, film, fileName, ChosenSeat, email);
            return reservationCompleterd;
        }
        else
        {
            string readJSON = File.ReadAllText(fileName);
            if (readJSON == "")
            {
                // file exists but is empty | do same as above
                bool reservationCompleterd = WriteReservationToEmptyFile(zaal, unavailavble_seats, datum, film, fileName, email: email);
                return reservationCompleterd;
            }
            else
            {
                // file exists and has content | read content add new reservation to content write new content
                StreamWriter writer = new(fileName);
                var reservations = JsonConvert.DeserializeObject<List<ReservationJsonObj>>(readJSON);

                // check if a reservation for this film already exists
                ReservationJsonObj existing_reservation = null!;
                foreach (ReservationJsonObj reservation in reservations!)
                {
                    if ($"{film.Name} {film.Director} {datum} {zaal.ID}" == reservation.ID)
                    {
                        //reservation with same id found
                        int seat_local = SeatSelection.SelectSeat(zaal.Size, reservation.Seats);

                        // function for calulatin price
                        // func for paying
                        if (false)
                        {
                            // return false if reservation failed
                            return false;
                        }

                        reservation.Seats.Add(seat_local);
                        reservation.Emails.Add(email);
                        var json = JsonConvert.SerializeObject(reservations);
                        writer.WriteLine(json);
                        writer.Close();

                        // return true if reservation succesfull
                        return true;
                    }
                }
                writer.Close();
                //reservation with same id not found
                bool reservationCompleterd = AddReservationToFile(reservations, film, datum, zaal, fileName, ChosenSeat, email);
                return reservationCompleterd;
            }

        }
    }

    private static bool WriteReservationToEmptyFile(Zaal zaal, List<int> unavailavble_seats,
    string datum, Film film, string fileName, int ChosenSeat = -1, string email = "NoEmail")
    {
        int seat = 0;

        if (ChosenSeat == -1)
        { seat = SeatSelection.SelectSeat(zaal.Size, unavailavble_seats); }
        else
        {
            seat = ChosenSeat;
        }

        // function for calculating cost
        // function for paying and chcne for user to cancel
        if (false)
        {
            // retruns false if user decided not to reserve
            return false;
        }

        List<int> seats = new() { seat };

        ReservationJsonObj reservation = new()
        {
            ID = $"{film.Name} {film.Director} {datum} {zaal.ID}",
            Date = datum,
            ZaalID = zaal.ID,
            Seats = new List<int> { seat },
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

    private static bool AddReservationToFile(List<ReservationJsonObj> reservations, Film film,
     string datum, Zaal zaal, string fileName, int seatForTesting = -1, string email = "NoEmail")
    {
        int seat = 0;
        if (seatForTesting == -1)
        {
            seat = SeatSelection.SelectSeat(zaal.Size, new List<int> { });
        }
        else
        {
            seat = seatForTesting;
        }

        // function for calculating cost
        // function for paying and chcne for user to cancel
        if (false)
        {
            // retruns false if user decided not to reserve
            return false;
        }

        ReservationJsonObj reservation = new()
        {
            ID = $"{film.Name} {film.Director} {datum} {zaal.ID}",
            Date = datum,
            ZaalID = zaal.ID,
            Seats = new List<int> { seat },
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

    public static void WriteReservationsToJSON(List<ReservationJsonObj> reservations, string fileName = "reservations.josn")
    {
        string json = JsonConvert.SerializeObject(reservations);
        File.WriteAllText(fileName, json);
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
}






