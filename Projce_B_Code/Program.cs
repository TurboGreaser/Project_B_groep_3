using Project_B;

namespace Priject_b;


public class Program
{
    public static void Main()
    {
        var films = JsonReader.ReadFilmJson();

        string date = "";
        foreach (var showing in films[0].Showings)
        {
            date = showing.Key;
            break;
        }

        Reservation.MakeReservation(films[0], date);
    }
}
