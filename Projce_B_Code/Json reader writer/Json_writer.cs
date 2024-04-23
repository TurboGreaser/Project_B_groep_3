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
    public static bool WriteReservationToJSON(Film film, Zaal zaal, string datum, int age, string fileName = "Reservations.json", int ChosenSeat = -1, string email = "NoEmail")
    {
        List<ReservationJsonObj> reservations = new() { };
        ReservationJsonObj existringReservation = null!;
        List<int> unavailavble_seats = new();
        bool emptyFile = false;

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
            // read file
            string readJson = File.ReadAllText(fileName);
            // make list of reservation objects
            reservations = JsonConvert.DeserializeObject<List<ReservationJsonObj>>(readJson)!;
            // look for reservation with same id
            foreach (ReservationJsonObj reservation in reservations!)
            {
                if ($"{film.Name} {film.Director} {datum} {zaal.ID}" == reservation.ID)
                {
                    unavailavble_seats = reservation.Seats;
                    existringReservation = reservation;
                }
            }
        }
        // now select your seat
        // skips this step if ChosenSeat is -1 this for unit testing
        if (ChosenSeat == -1)
        {
            ChosenSeat = SeatSelection.SelectSeat(zaal.Size, unavailavble_seats);
            if (ChosenSeat == -1)
            { return false; }
        // pay for your seat
            double price = SeatSaleRoom.GetMoviePrice(film.Name);
            double seatFee = SeatSaleRoom.IsExpensive(zaal.Size, ChosenSeat) ? price * 0.1 : 0; // 10% etra for expensive seat
            double ageFee = age < 18 ? price * 0.2 : 0; // 20% extra for inder 18 customer
            // if customer doesnt pay return false
            if (!Reservation.PrintPrice(price, seatFee, ageFee))
            { return false; }
        }



        // write reservation to json
        if (emptyFile)
        {
            // write new file / write to empty file
            return WriteReservationToEmptyFile(zaal, datum, film, fileName, ChosenSeat, email);
        }
        else if (existringReservation != null)
        {
            // add seat and email to reservation and write the updated list
            existringReservation.Seats.Add(ChosenSeat);
            existringReservation.Emails.Add(email);
            var json = JsonConvert.SerializeObject(reservations);
            return WriteReservationsToJSON(reservations, fileName);
        }
        else
        {
            // add new reservation to file
            return AddNewReservationToFile(reservations, film, datum, zaal, fileName, ChosenSeat, email);
        }

    }

    private static bool WriteReservationToEmptyFile(Zaal zaal, string datum, Film film, string fileName, int ChosenSeat, string email = "NoEmail")
    {
        ReservationJsonObj reservation = new()
        {
            ID = $"{film.Name} {film.Director} {datum} {zaal.ID}",
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
            ID = $"{film.Name} {film.Director} {datum} {zaal.ID}",
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
}






