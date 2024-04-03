namespace Project_B;

public static class Reservation
{
    public static void MakeReservation(Film film, string date)
    {
        Zaal zaal = GetZaalFromFilm(film, date);



        Console.WriteLine($"Je maakt een reservering voor: {film.Name} door: {film.Director}");
        Console.WriteLine($"Op {date} in zaal {zaal.ID}");
        Console.WriteLine($"klik Enter om veder te gaan naar het kiezen van je stoel");
        Console.ReadLine();

        Json_writer.WriteReservationToJSON(film, zaal, date);
    }

    private static Zaal GetZaalFromFilm(Film film, string date)
    {
        List<Zaal> zalen = JsonReader.ReadZalen();



        foreach (Zaal zaal in zalen)
        {
            foreach (var showing in film.Showings)
            {
                if (showing.Key == date && showing.Value == zaal.ID)
                {
                    return zaal;
                }
            }
        }

        Console.WriteLine("Film heeft geen Showings Default zaal wordt gebruikt");
        return zalen[0];
    }
}


