using Newtonsoft.Json;
using System.Globalization;

public static class SaleMovie
{
    private const int SalePercent = 20;
    public static double TimeSaleStart = 30;


    public static double CalculateSale(string moviename, DateTime CurrentTime = default)
    {
        string jsonfilepathfilms = "Films.json";
        string text = File.ReadAllText(jsonfilepathfilms);
        List<Film>? films = JsonConvert.DeserializeObject<List<Film>>(text);

        Film movie = films.Find(film => film.Name == moviename); // film zoeken

        

        double discountAmount = movie.Price - (movie.Price * SalePercent / 100); // berekent korting
        return discountAmount; // geeft de prijs terug

        // double testprijs = 5; 
        // double korting_test_prijs = SaleMovie.CalculateSale(testprijs);
    }
}
