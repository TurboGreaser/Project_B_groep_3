namespace Project_B;

using Newtonsoft.Json;


public static class Json_writer
{
    //     public static void WriteFilmToJSON(Film film)
    //     {
    //         string fileName = "Films.json";
    //         StreamWriter writer = new(fileName);


    //         writer.Write(JsonConvert.SerializeObject(film));
    //         writer.Close();
    //     }
    // }

    public static void WriteFilmToJSON(Film film)
    {
        string fileName = "Films.json";
        StreamWriter writer = new(fileName);

        List<Film> _films = new() { film };
        // _films.Add();

        var json = JsonConvert.SerializeObject(_films.ToArray());
        // var json = JavaScriptSerializer.Serialize(data);
        // Console.WriteLine(json);
        writer.WriteLine(json);
        writer.Close();



    }
}
