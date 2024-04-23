public class Film
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public int Duration_in_minutes { get; set; }
    public double Price { get; set; }
    public string Director { get; set; }
    public string Description { get; set; }
    public Dictionary<string, int> Showings { get; set; }
}