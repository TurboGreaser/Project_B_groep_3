using System.ComponentModel.DataAnnotations;
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

    public static string Search(List<Film> Filmlist, string Name)
    {
        string? output = "";
        foreach (Film film in Filmlist)
        {
            if (film.Name == Name)
            {
                output += film.CompactInfo();
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
}
