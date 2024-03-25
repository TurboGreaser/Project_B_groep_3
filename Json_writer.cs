namespace Project_B;

using Newtonsoft.Json;


public static class Json_writer
{

    public static void WriteFilmToJSON(Film film)
    {
        string fileName = "bin/Films.json";
        StreamWriter writer = new(fileName);

        List<Film> _films = new() { film };
        var json = JsonConvert.SerializeObject(_films.ToArray());
        writer.WriteLine(json);
        writer.Close();



    }
}
