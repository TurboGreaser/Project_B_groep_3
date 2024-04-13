class SeatSaleRoom
{   
    public static string jsonfilepathfilms = "Films.json";

    public static double GetMoviePrice(string movieName)
    {
        string text = File.ReadAllText(jsonfilepathfilms);
        var films = JsonConvert.DeserializeObject<List<Film>>(text);

        Film movie = films.Find(film => film.Name == movieName); // film zoeken

        if (movie != null)
        {
            return movie.Price;
        }
        else
        {
            return 0;
        }
    }
}


