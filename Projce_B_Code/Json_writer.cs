namespace Project_B;

using Newtonsoft.Json;


public static class Json_writer
{

    public static void WriteFilmToJSON(Film film)
    {
        string fileName = "Films_test.json";
        StreamWriter writer = new(fileName);

        List<Film> _films = new() { film };
        var json = JsonConvert.SerializeObject(_films.ToArray());
        writer.WriteLine(json);
        writer.Close();
    }

    public static void WriteReservationToJSON(Film film, Zaal zaal, string datum)
    {
        List<int> unavailavble_seats = new();
        string fileName = "Reservations.json";


        if (!File.Exists(fileName))
        {
            // file doest exists | make new file write reservation to new file
            WriteReservationToEmptyFile(zaal, unavailavble_seats, datum, film, fileName);
        }
        else
        {
            string readJSON = File.ReadAllText(fileName);
            if (readJSON == "")
            {
                // file exists but is empty | do same as above
                WriteReservationToEmptyFile(zaal, unavailavble_seats, datum, film, fileName);
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
                    if ($"{film.Name}{film.Director}{datum}{zaal.ID}" == reservation.ID)
                    {
                        //reservation with same id found
                        int seat_local = SeatSelection.SelectSeat(zaal.Size, reservation.Seats);
                        reservation.Seats.Add(seat_local);
                        var json = JsonConvert.SerializeObject(reservations);
                        writer.WriteLine(json);
                        writer.Close();
                        return;
                    }
                }
                writer.Close();
                //reservation with same id not found
                AddReservationToFile(reservations, film, datum, zaal, fileName);
            }

        }
    }

    private static void WriteReservationToEmptyFile(Zaal zaal, List<int> unavailavble_seats, string datum, Film film, string fileName)
    {
        int seat = SeatSelection.SelectSeat(zaal.Size, unavailavble_seats);
        List<int> seats = new() { seat };

        ReservationJsonObj reservation = new()
        {
            ID = $"{film.Name}{film.Director}{datum}{zaal.ID}",
            Date = datum,
            ZaalID = zaal.ID,
            Seats = new List<int> { seat }
        };


        StreamWriter writer = new(fileName);
        List<ReservationJsonObj> list_of_reservations = new() { reservation };
        var json = JsonConvert.SerializeObject(list_of_reservations);
        writer.WriteLine(json);
        writer.Close();
    }

    private static void AddReservationToFile(List<ReservationJsonObj> reservations, Film film, string datum, Zaal zaal, string fileName)
    {

        int seat = SeatSelection.SelectSeat(zaal.Size, new List<int> { });
        ReservationJsonObj reservation = new()
        {
            ID = $"{film.Name}{film.Director}{datum}{zaal.ID}",
            Date = datum,
            ZaalID = zaal.ID,
            Seats = new List<int> { seat }
        };
        reservations.Add(reservation);
        StreamWriter writer = new(fileName);
        var json = JsonConvert.SerializeObject(reservations);
        writer.WriteLine(json);
        writer.Close();

    }

    private class ReservationJsonObj
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public int ZaalID { get; set; }
        public List<int> Seats { get; set; } // This will be serialized as an array in JSON
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






