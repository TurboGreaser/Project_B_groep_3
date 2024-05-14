using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public static class SaleMovie
{
    private const int SalePercent = 20;
    private const int SaleStartHoursBefore = 3;

    public static (double Price, int SalePercent, DateTime SaleStartTime)? GetSaleDetails(string movieName, DateTime currentTime = default)
    {
        string jsonFilePath = "Films.json";
        string text = File.ReadAllText(jsonFilePath);
        List<Film> films = JsonConvert.DeserializeObject<List<Film>>(text);

        Film movie = films.Find(film => film.Name == movieName);

        if (movie == null)
            return null; // Movie not found

        var upcomingShowings = movie.Showings
            .Where(kv => DateTime.ParseExact(kv.Key, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) > currentTime)
            .OrderBy(kv => DateTime.ParseExact(kv.Key, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture))
            .ToList();

        if (upcomingShowings.Count == 0)
            return null; // No upcoming showings

        var nextShowing = upcomingShowings.First();
        DateTime saleStartTime = DateTime.ParseExact(nextShowing.Key, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            .AddHours(-SaleStartHoursBefore);

        if (saleStartTime <= currentTime)
            return null; // Sale already started or within 3 hours

        double salePrice = movie.Price - (movie.Price * SalePercent / 100);

        return (salePrice, SalePercent, saleStartTime);
    }
}
