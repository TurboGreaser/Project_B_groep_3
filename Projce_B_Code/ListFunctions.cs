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
            Console.WriteLine(film.CompactInfo());
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
        Display(filmList);
        while (true)
        {
            Console.WriteLine("Voer in de titel van de film die je wil kiezen:");
            string? choice = Console.ReadLine();

            if (choice == null)
            {
                Console.WriteLine("verkeerde input. Voer in de titel van de film die je wil kiezen:");
                continue;
            }

            Film chosenFilm = filmList.Find(x => x.Name == choice);
            
            if (chosenFilm != null)
            {
                return chosenFilm;
            }
            else
            {
                Console.WriteLine("verkeerde input. Voer in de titel van de film die je wil kiezen:");
            }
        }
    }
}
