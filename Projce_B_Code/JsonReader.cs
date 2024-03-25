namespace Project_B;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class JsonReader
{
    public static void ReadFilmJson()
    {
        // Read the JSON file into a string
        string jsonFilePath = "Films.json";
        // string jsonFilePath = "C:/Users/Ivan/Documents/Project_b/Projce_B_Code/Films.json";

        string jsonText = File.ReadAllText(jsonFilePath);

        // var data = JsonConvert.DeserializeObject<List<object>>(jsonText);

        List<Film> data = JsonConvert.DeserializeObject<List<Film>>(jsonText);

        foreach (var film in data)
        {
            Console.WriteLine(film.Info());
        }

    }
}