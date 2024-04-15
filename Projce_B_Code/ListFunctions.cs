using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Project_B;
public static class ListFunctions
{
    public static List<Film> SortList(List<Film> films, string sortBy, bool asc = false)
    {
        switch (sortBy)
        {
            case "Name":
                return asc ? films.OrderBy(film => film.Name).ToList() : films.OrderByDescending(film => film.Name).ToList();
            case "Genre":
                return asc ? films.OrderBy(film => film.Genre).ToList() : films.OrderByDescending(film => film.Genre).ToList();
            case "Duration_in_minutes":
                return asc ? films.OrderBy(film => film.Duration_in_minutes).ToList() : films.OrderByDescending(film => film.Duration_in_minutes).ToList();
            case "Price":
                return asc ? films.OrderBy(film => film.Price).ToList() : films.OrderByDescending(film => film.Price).ToList();
            case "Director":
                return asc ? films.OrderBy(film => film.Director).ToList() : films.OrderByDescending(film => film.Director).ToList();
            case "Description":
                return asc ? films.OrderBy(film => film.Description).ToList() : films.OrderByDescending(film => film.Description).ToList();
            default:
                return films;
        }
    }

    public static void Display(List<Film> Filmlist)
    {
        foreach (Film film in Filmlist)
        {
            Console.WriteLine($"{Filmlist.IndexOf(film) + 1}. {film.CompactInfo()}");
        }
    }

    public static string Search(List<Film> Filmlist, string searchTerm)
{
    string output = "";
    searchTerm = searchTerm.ToLower(); 
    foreach (Film film in Filmlist)
    {
        if (film.Name.ToLower().StartsWith(searchTerm))
        {
            output += film.CompactInfo() + "\n"; 
        }
    }
    return output;
}


    public static Film ChooseFilm(List<Film> filmList)
    {
        Console.Clear();
        Display(filmList);
        while (true)
        {
                try
                {
                    Console.WriteLine("Voer in het nummer van de film die je wil kiezen:");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice > filmList.Count || choice <= 0)
                    {
                        Console.WriteLine($"Verkeerde input. Kies een nummer tussen 1 en {filmList.Count}");
                        continue;
                    }
                    if (choice == null)
                    {
                        Console.WriteLine("Verkeerde input. Voer in het nummer van de film die je wil kiezen:");
                        continue;
                    }

                    Film chosenFilm = filmList[choice - 1];
                    
                    if (chosenFilm != null)
                    {
                        return chosenFilm;
                    }
                    else
                    {
                        Console.WriteLine("Verkeerde input. Voer in het nummer van de film die je wil kiezen:");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Verkeerde input. Voer in het nummer van de film die je wil kiezen");
                    continue;
                }

        }
    }

   public static Dictionary<string, int> ChooseShowing(Film film)
    {
        Console.WriteLine($"Wanneer kunt u de film {film.Name} kijken?");
        int index = 1;
        foreach (var showing in film.Showings)
        {
            Console.WriteLine($"{index}. Datum: {showing.Key}, Tijden: {showing.Value}");
            index++;
        }

        Console.WriteLine("Voer het nummer in van de tijd die u wilt kiezen");
        int chosenIndex;
        while (!int.TryParse(Console.ReadLine(), out chosenIndex) || chosenIndex < 1 || chosenIndex > film.Showings.Count)
        {
            Console.WriteLine($"Kies aub een nummer tussen 1 en {film.Showings.Count}");
        }

        var selectedShowing = film.Showings.ElementAt(chosenIndex - 1);
        return new Dictionary<string, int> { { selectedShowing.Key, selectedShowing.Value } };
    }
}
//     public static  Dictionary<string, int> ChooseShowing(Film film)
//     {
//         while (true)
//         {
//             Console.WriteLine("Wanneer kunt u de film {film.Name} kijken?");
//             Dictionary<string, int> Showings = film.Showings;
//             List<KeyValuePair <string,int>> ShowingsList = [.. Showings]; //Dictionary naar een list converten zodat je de index kan krijgen
//             foreach (var showing in ShowingsList)
//             {
//                 Console.WriteLine($"{ShowingsList.IndexOf(showing) + 1}. Datum: {showing.Key} --- Zaal: {showing.Value}");
//             }
//             Console.WriteLine("Voor welke datum wilt u de film reserveren?");
//             try
//             {
//                 int choice = Convert.ToInt32(Console.ReadLine());
//                 if (choice <= 0 || choice > ShowingsList.Count)
//                 {
//                     Console.WriteLine($"Kies een getal tussen 1 en {ShowingsList.Count}");
//                     continue;
//                 }
//                 Console.WriteLine($"U hebt voor de datum: {ShowingsList[choice].Key} gekozen In zaal: {ShowingsList[choice].Value}");
//                 Dictionary<string, int> output = new();

//             }
//             catch (FormatException)
//             {
//                 Console.WriteLine($"Kies een getal tussen 1 en {ShowingsList.Count}");
//                 continue;
//             }

//             break;


//         }
//     }
// }
