using Newtonsoft.Json;
using System;
namespace Project_B;
using System.Globalization;


public static class SaleMovie
{
    private const int SalePercent = 20;
    private const int SaleStartHoursBefore = 3;

    public static double GetSaleDetails(string movieName, DateTime currentTime = default)
    {
        string jsonFilePath = "Films.json";
        string text = File.ReadAllText(jsonFilePath);
        List<Film> films = JsonConvert.DeserializeObject<List<Film>>(text);

        Film movie = films.Find(film => film.Name == movieName);

        if (movie == null)
            return 0; 

        var upcomingShowings = movie.Showings // kijkt hvl film speelt, min -3 uur en plaatst in list
            .Select(showing => DateTime.ParseExact(showing.Datum, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture).AddHours(-SaleStartHoursBefore))
            .OrderBy(showing => showing)
            .ToList();

        bool isDiscountApplied = false;
        DateTime saleStartTime = DateTime.MaxValue;

        foreach (var showing in upcomingShowings)
        {
            DateTime movieStartTime = showing.AddHours(SaleStartHoursBefore); // zet showing naar begin tijd van film

            if (currentTime >= showing && currentTime < movieStartTime) // checkt of tijd van user is tussen sale begin en film begin
            {
                saleStartTime = showing;
                isDiscountApplied = true;
                break;
            }
        }


        if (isDiscountApplied)
        {
            double salePrice = movie.Price - (movie.Price * SalePercent / 100);
            return Math.Round(salePrice, 2); 
        }
        else
        {
            return Math.Round(movie.Price, 2); 
        }
    }
}
