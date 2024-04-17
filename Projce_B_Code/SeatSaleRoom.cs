using Project_B;
using Newtonsoft.Json;
public static class SeatSaleRoom
{
    public static double GetMoviePrice(string movieName)
    {
        string jsonfilepathfilms = "Films.json";
        string text = File.ReadAllText(jsonfilepathfilms);
        List<Film>? films = JsonConvert.DeserializeObject<List<Film>>(text);

        Film movie = films.Find(film => film.Name == movieName); // film zoeken

        if (movie != null)

            return movie.Price;
        return 0;
    }



    public static List<int> GetMiddleSeats(int size)
    {
        // int Seats = (int)Math.Pow(size, 2);
        int Half = size / 2;
        int quad;
        if (size % 2 == 0)
        {
            quad = size / 4;
        }

        else
        {
            quad = size / 3;
            if (quad == 1)
            {
                quad++;
            }
        }
        List<int> FirstRow = new();
        int Rightmid = Half + quad;
        List<int> MiddleSeats = new();
        for (int i = quad; i <= Rightmid; i++)
        {
            FirstRow.Add(i);
            MiddleSeats.Add(i);
        }
        int iter = size;
        for (int i = 0; i < Half - 1; i++)
        {
            foreach (int j in FirstRow)
            {
                MiddleSeats.Add(j + iter);
            }
            iter += size;
        }

        return MiddleSeats;
    }

    public static bool IsExpensive(int size, int seat)
    {
        List<int> SeatsList = GetMiddleSeats(size);
        return SeatsList.Contains(seat);
    }

}