using Project_B;

namespace Priject_b;


public class Program
{
    public static void Main()
    {
        // format 2022-12-30 10:30 : year-month-day hour:minute

        Dictionary<string, int> test_showings = new() { { "2022-12-30 10:30", 1 }, { "2022-10-20 09:30", 1 } };


        Film test_film = new("test_name", "test_genre", 75, 12.50, "test_director", "test descrition", test_showings);
        // Json_writer.WriteFilmToJSON(test_film);
        // Console.WriteLine(test_film.Info());
        JsonReader.ReadFilmJson();

    }
}
//