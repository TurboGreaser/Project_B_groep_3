using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace Project_B;
public static class ListFunctions
{
    public static List<Film> SortList(List<Film> films, string sortBy, bool asc = false)
    {
        string OrderBy;
        if (sortBy.Contains("naam"))
        {
            OrderBy = "Name";
        }
        else if (sortBy.Contains("genre"))
        {
            OrderBy = "Genre";
        }
        else if (sortBy.Contains("duur"))
        {
            OrderBy = "Duration_in_minutes";
        }
        else if (sortBy.Contains("prijs") || sortBy.Contains("Price"))
        {
            OrderBy = "Price";
        }
        else if (sortBy.Contains("regisseur"))
        {
            OrderBy = "Director";
        }
        else
        {
            OrderBy = "Description";
        }
        switch (OrderBy)
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

    public static List<Film> Search(List<Film> Filmlist, string Name)
    {
        List<Film>? NewList = new() { };
        foreach (Film film in Filmlist)
        {
            string Title = film.Name.ToUpper();
            if (Title.Contains(Name.ToUpper()))
            {
                NewList.Add(film);
            }
        }
        return NewList;
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
                    Console.WriteLine($"Kies Aub een nummer tussen 1 en {filmList.Count}");
                    continue;
                }
                if (choice == null)
                {
                    Console.WriteLine("");
                    continue;
                }

                Film chosenFilm = filmList[choice - 1];

                if (chosenFilm != null)
                {
                    Console.WriteLine($"U hebt voor de film '{chosenFilm.Name}' gekozen");
                    return chosenFilm;
                }
                else
                {
                    Console.WriteLine("");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("");
                continue;
            }

        }
    }


    public static Showing ChooseShowing(Film film)
    {
        int IndexOfCurrentOption = 0;
        while (true)
        {
            Console.Clear();
            List<Showing> Showings = film.Showings;
            foreach (var showing in Showings)
            {
                int index = Showings.IndexOf(showing);
                if (index == IndexOfCurrentOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{index + 1}. Datum: {showing.Datum}, Zaal: {showing.Zaal}   <-- \n");

                }
                else
                {
                    Console.WriteLine($"{index + 1}. Datum: {showing.Datum}, Zaal: {showing.Zaal}");
                }

                Console.ResetColor();
            }
            ConsoleKeyInfo KeyInput = Console.ReadKey(true);
            switch (KeyInput.Key)
            {
                case ConsoleKey.UpArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == 0) ? Showings.Count - 1 : IndexOfCurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == Showings.Count - 1) ? 0 : IndexOfCurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    return Showings[IndexOfCurrentOption];
            }
        }
    }
}