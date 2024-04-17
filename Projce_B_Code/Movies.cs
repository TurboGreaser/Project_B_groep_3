using Newtonsoft.Json;
using Project_B;


public static class Movies
{
    public static void ShowMoviesToday()
    {
        // Datum van vandaag ophalen
        DateTime today = DateTime.Today;

        // JSON-bestand lezen
        string json = File.ReadAllText("films.json");

        // Films deserialiseren vanuit JSON
        List<Film> films = JsonConvert.DeserializeObject<List<Film>>(json);

        // Films voor vandaag identificeren en afdrukken
        Console.WriteLine("Films die vandaag draaien:");

        foreach (Film film in films)
        {
            foreach (KeyValuePair<string, int> showing in film.Showings)
            {
                DateTime showingDate = DateTime.Parse(showing.Key);

                if (showingDate.Date == today)
                {
                    Console.WriteLine($"- {film.Name}");
                    break;
                }
            }
        }

        Console.ReadLine();
    }
}


