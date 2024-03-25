namespace Project_B;

using Newtonsoft.Json;

public static class JsonReader
{
    public static void ReadFilmJson()
    {
        // Read the JSON file into a string
        // string jsonFilePath = "Films.json"; // Replace with your JSON file path
        string jsonFilePath = "Films.json"; // Replace with your JSON file path
        string jsonText = File.ReadAllText(jsonFilePath);

        var data = JsonConvert.DeserializeObject<List<object>>(jsonText);

        // Console.WriteLine(data);
        foreach (var item in data)
        {
            Console.WriteLine(item);
        }
    }
}