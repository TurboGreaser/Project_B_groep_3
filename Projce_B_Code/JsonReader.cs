namespace Project_B;

using Newtonsoft.Json;

public static class JsonReader
{
    public static List<Film> ReadFilmJson()
    {
        // Read the JSON file into a string
        string jsonFilePath = "Films.json";
        string jsonText = File.ReadAllText(jsonFilePath);

        List<Film> List_of_films = JsonConvert.DeserializeObject<List<Film>>(jsonText);
        return List_of_films;

    }
}