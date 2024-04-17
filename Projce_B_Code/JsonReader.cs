namespace Project_B;

using Newtonsoft.Json;

public static class JsonReader
{
    public static List<Film> ReadFilmJson(string jsonFilePath = "Films.json")
    {
        // Read the JSON file into a string
        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            List<Film> List_of_films = JsonConvert.DeserializeObject<List<Film>>(jsonText);
            return List_of_films;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not open the json file");
            Console.WriteLine(ex.Message);
            return null;
        }

    }

    public static List<Zaal> ReadZalen()
    {
        string fileName = "Zaalen.json";
        if (File.Exists(fileName))
        {
            string jsonText = File.ReadAllText(fileName);

            List<Zaal> List_of_Zaalen = JsonConvert.DeserializeObject<List<Zaal>>(jsonText);
            return List_of_Zaalen;
        }
        return null;
    }

    public static List<Json_writer.ReservationJsonObj> ReadReservations(string FileName = "Reservations.json")
    {
        string jsonText = File.ReadAllText(FileName);
        List<Json_writer.ReservationJsonObj> reservations = JsonConvert.DeserializeObject<List<Json_writer.ReservationJsonObj>>(jsonText)!;

        return reservations;
    }

    public static List<Accounts> ReadAccounts(string FileName = "Accounts.json")
    {
        string josnText = File.ReadAllText(FileName);
        List<Accounts> accounts = JsonConvert.DeserializeObject<List<Accounts>>(josnText)!;
        return accounts;
    }
}
