// using Newtonsoft.Json;

// namespace Project_B;

// public static class Movies
// {
//     public static List<Film> ShowMoviesToday(DateTime Currentime = default, string fileName = "Films.json")
//     {
//         List<Film> filmstoday = new List<Film>();

//         if (Currentime == default)
//         { Currentime = DateTime.Now; }

//         // JSON-bestand lezen
//         string json = File.ReadAllText(fileName);

//         // Films voor vandaag identificeren en afdrukken
//         Console.WriteLine("Films die vandaag draaien:");

//         List<Film> films = JsonConvert.DeserializeObject<List<Film>>(json);

//         foreach (Film film in films)
//         {
//             foreach (KeyValuePair<string, int> showing in film.Showings)
//             {
//                 DateTime showingDate = DateTime.Parse(showing.Key);
//                 if (showingDate.Date == Currentime)
//                 {
//                     Console.WriteLine($"- {film.Name}");
//                     filmstoday.Add(film);
//                 }
//             }
//         }
//         if (fileName == "films.json")
//         {
//             Console.WriteLine("Klik enter om veder te gaan");
//             Console.ReadLine();
//         }

//         return filmstoday;
//     }
// }
using Newtonsoft.Json;

namespace Project_B
{
    public static class Movies
    {
        public static void PrintMoviesToday()
        {
            // Roep ShowMoviesToday aan en druk de films die vandaag spelen af
            List<Film> filmsToday = ShowMoviesToday();
            Console.WriteLine("Films die vandaag draaien:");
            foreach (Film film in filmsToday)
            {
                Console.WriteLine($"- {film.Name}:");
                foreach (KeyValuePair<string, int> showing in film.Showings)
                {
                    DateTime showingTime = DateTime.Parse(showing.Key);
                    Console.WriteLine($"    {showingTime.ToString("HH:mm")}"); // Toon alleen de tijd
                }
            }

            Console.WriteLine("Klik enter om verder te gaan");
            Console.ReadLine();
        }

        private static List<Film> ShowMoviesToday(DateTime currentTime = default, string fileName = "Films.json")
        {
            List<Film> filmsToday = new List<Film>();

            if (currentTime == default)
            {
                currentTime = DateTime.Now;
            }

            // JSON-bestand lezen
            string json = File.ReadAllText(fileName);

            // Films voor vandaag identificeren
            List<Film> films = JsonConvert.DeserializeObject<List<Film>>(json);

            foreach (Film film in films)
            {
                foreach (KeyValuePair<string, int> showing in film.Showings)
                {
                    DateTime showingDate = DateTime.Parse(showing.Key);
                    if (showingDate.Date == currentTime.Date)
                    {
                        filmsToday.Add(film);
                    }
                }
            }

            return filmsToday;
        }
    }
}


