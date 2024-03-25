namespace Project_B;

public class Film
{
    public string Name;
    public string Genre;
    public int Duration_in_minutes;
    public double Price;
    public string Director;
    public string Description;
    public List<Showing> Showings
    {
        set;
        get;
    }

    public Film(string name, string genre, int duration, double price, string director, string description, List<Showing> showings)
    {
        Name = name;
        Genre = genre;
        Duration_in_minutes = duration;
        Price = price;
        Director = director;
        Description = description;
        Showings = showings;
    }

    public string Info()
    {
        return
        $"Naam: {Name}\n" +
        $"Lengte: {Duration_in_minutes} min\n" +
        $"Ganre: {Genre}\n" +
        $"Director: {Director}\n" +
        $"\nBeschrijving:\n{Description}\n" +
        $"\nFilm wordt afgespeeld op deze tijden: {Showings}\n" +
        $"Prijs: {Price} Euro\n" +
        $"Je kan hem zien op:\n" +
        $"{FormatShowings()}";
    }

    private string FormatShowings()
    {
        if (Showings.Count > 0)
        {
            string formattedShowings = "";
            foreach (Showing showing in Showings)
            {
                formattedShowings += showing + "\n";
            }
            return formattedShowings;
        }
        return "geen tijden gevonden";
    }





}
