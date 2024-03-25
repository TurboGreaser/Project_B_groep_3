using Project_B;

namespace Priject_b;


public class Program
{
    public static void Main()
    {   
        Film test_film = new("test_name", "test_genre", 75, 12.50, "test_director", "test descrition", new List<Showing> { });
        // Json_writer.WriteFilmToJSON(test_film);
        Console.WriteLine(test_film.Info());
    }
}
//