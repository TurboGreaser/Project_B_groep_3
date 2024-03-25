using Project_B;

namespace Priject_b;


public class Program
{
    public static void Main()
    {
        // Zaal zaal_1 = new(2, 5);

        // // format 2022-12-30 10:30 : year-month-day hour:minute
        // Showing test_showing = new(zaal_1, "2024-06-15 11:45");
        // Showing test_showing_2 = new(zaal_1, "2025-08-25 12:45");

        // Film test_film = new("test_name", "test_genre", 75, 12.50, "test_director", "test descrition", new List<Showing> {test_showing, test_showing_2 });
        // // Json_writer.WriteFilmToJSON(test_film);
        // Console.WriteLine(test_film.Info());
        JsonReader.ReadFilmJson();

    }
}
//