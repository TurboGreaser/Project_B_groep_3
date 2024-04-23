<<<<<<< HEAD
// using Newtonsoft.Json;
// namespace Project_B;

// public class Movies
// {
//     public static void Main()
//     {
//         // Datum van vandaag ophalen
//         DateTime today = DateTime.Today;

//         // JSON-bestand lezen
//         string json = File.ReadAllText("films.json");

//         // Films deserialiseren vanuit JSON
//         List<Film> films = JsonConvert.DeserializeObject<List<Film>>(json);
=======
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;

namespace Project_B;

public static class Movies
{
    public static List<Film> ShowMoviesToday(DateTime Currentime = default, string fileName = "films.json")
    {
        List<Film> filmstoday = new List<Film>();

        if (Currentime == default)
        { Currentime = DateTime.Now; }

        // JSON-bestand lezen
        string json = File.ReadAllText(fileName);
>>>>>>> main

//         // Films voor vandaag identificeren en afdrukken
//         Console.WriteLine("Films die vandaag draaien:");

//         foreach (Film film in films)
//         {
//             foreach (KeyValuePair<string, int> showing in film.Showings)
//             {
//                 DateTime showingDate = DateTime.Parse(showing.Key);

//                 if (showingDate.Date == today)
//                 {
//                     Console.WriteLine($"- {film.Name}");
//                     break;
//                 }
//             }
//         }

<<<<<<< HEAD
//         Console.ReadLine();
//     }
// }

// public class Film
// {
//     public string Name { get; set; }
//     public Dictionary<string, int> Showings { get; set; }
// }
=======
                if (showingDate.Date == Currentime)
                {
                    Console.WriteLine($"- {film.Name}");
                    filmstoday.Add(film);
                }
            }
        }
        if (fileName == "films.json")
        {
            Console.WriteLine("Klik enter om veder te gaan");
            Console.ReadLine();
        }

        return filmstoday;
    }
}


>>>>>>> main
