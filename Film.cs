namespace Project_B;

public class Film
{
    private string Name;
    private string Genre;
    private int Duration_in_minutes;
    private double Price;
    private string Director;
    private string Description;
    private int Auditorium;
    // Auditorium = zaal waar je de film kijkt
    private List<DateTime> Showings;

    public Film(string name, string genre, int duration, double price, string director, string description, int auditorium, List<DateTime> showings)
    {
        Name = name;
        Genre = genre;
        Duration_in_minutes = duration;
        Price = price;
        Director = director;
        Description = description;
        Auditorium = auditorium;
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
        $"\nFilm wordt afgespeeld op deze tijden: {Showings}" +
        $"\nZaal: {Auditorium}" +
        $"Prijs: {Price}€\n";
    }





}
