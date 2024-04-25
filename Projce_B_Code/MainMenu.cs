using Newtonsoft.Json;
namespace Project_B;
static class MainMenu

{
    public static List<Film> films = JsonReader.ReadFilmJson();
    public static List<Film> SortedFilm = ListFunctions.SortList(films, "Price");

    // public static void ShowMenu()
    // {
    //     bool ValidInput;
    //     string? Choice;
    //     Accounts account = new Accounts()
    //     {
    //         Age = 18
    //     };
    //     do
    //     {
    //         Console.WriteLine("1. Films bekijken\n2. Reserveren\n3. inloggen/Account maken\n4. Menu bioscoop restaurant bekijken\n5. Quit");
    //         try
    //         {
    //             Choice = Console.ReadLine();
    //             ValidInput = true;
    //             switch (Choice)
    //             {
    //                 case "1":
    //                     Console.WriteLine("===De film lijst wordt geopend..===");
    //                     ListFunctions.Display(ListFunctions.SortList(films, "Price"));
    //                     Choose(films);
    //                     break;

    //                 case "2":
    //                     Console.WriteLine("===Reserveren===");
    //                     MainFunctions.MakeNewReservation(account);
    //                     break;

    //                 case "3":
    //                     Console.WriteLine("===Accounts===");
    //                     Accounts account1 = AccountMenuQ.Choose();

    //                     if (account1 != null)
    //                     {
    //                         account = account1;
    //                     }
    //                     break;

    //                 case "4":
    //                     Console.WriteLine("===Resturant menu===");
    //                     MenuStore menu = new MenuStore();
    //                     menu.PrintMenu();
    //                     break;

    //                 case "5":
    //                     ValidInput = false;
    //                     break;

    //                 default:
    //                     Console.WriteLine("Kies tussen 1-5!");
    //                     ValidInput = true;
    //                     break;
    //             }
    //         }
    //         catch (IOException e)
    //         {
    //             Console.WriteLine($"Invalid input! {e.Message}");
    //             ValidInput = false;
    //         }

    //         catch (Exception e)
    //         {
    //             Console.WriteLine($"Invalid input! {e.Message}");
    //             ValidInput = false;
    //         }

    //     } while (ValidInput);
    // }
    public static void ShowFilmList()
    {
        int IndexOfCurrentOption = 0;
        while (true)
        {
            Console.Clear();
            foreach (Film film in SortedFilm)
            {
                int index = films.IndexOf(film);
                if (index == IndexOfCurrentOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{index + 1}. {film.CompactInfo()}   <-- \n");

                }
                else
                {
                    Console.WriteLine($"{index + 1}. {film.CompactInfo()}");
                }
                
                Console.ResetColor();
            }
            Console.WriteLine("\n\nDruk op 'S' om te sorteren       Druk op 'Z' om te zoeken ");
            ConsoleKeyInfo KeyInput = Console.ReadKey(true);
            switch(KeyInput.Key)
            {
                case ConsoleKey.UpArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == 0)? SortedFilm.Count -1 : IndexOfCurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == SortedFilm.Count -1)? 0 : IndexOfCurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    return;

                case ConsoleKey.S:
                    Console.WriteLine("Sort functie wordt geroepen");
                    break;
                case ConsoleKey.Z:
                    Console.WriteLine("Zoek functie wordt geroepen");
                    break;
            }
            return;
        }
    }

    public static void ShowMenu()
    {
        string[] MenuOptions = { "1. Reserveren", "2. Aanmelden", "3. Restrant menu", "4. Quit"};
        int IndexOfCurrentOption = 0;
        while (true)
        {
            Console.Clear();
            for (int i = 0; i < MenuOptions.Length; i++)
            {
                if (i == IndexOfCurrentOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("--> ");
                }
                else
                {
                    Console.Write("    ");
                }
                Console.WriteLine(MenuOptions[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo KeyInput = Console.ReadKey(true);
            switch (KeyInput.Key)
            {
                case ConsoleKey.UpArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == 0)? MenuOptions.Length -1 : IndexOfCurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == MenuOptions.Length -1)? 0 : IndexOfCurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    if (IndexOfCurrentOption == MenuOptions.Length-1)
                    {
                        Console.WriteLine("Het programma wordt gesloten");
                        return;
                    }
                    Console.WriteLine($"U hebt voor {MenuOptions[IndexOfCurrentOption]} gekozen");
                    ShowFilmList();
                    return;
                    // switch (Choice)
                    // {
                    //     case "Reserveren":
                    //     {
                    //         Console.WriteLine("===De film lijst wordt geopend..===");
                    //         ListFunctions.Display(ListFunctions.SortList(films, "Price"));
                    //         Choose(films);
                    //         break;
                    //     }
                }
        }
    }


    public static List<Film> SearchForFilm(List<Film> filmList)
    {
        bool ValidInput = false;
        List<Film> NewList = new();
        do
        {
            Console.WriteLine("Voer de title van de film in:");
            string? Choice = Console.ReadLine();
            try
            {
                NewList = ListFunctions.Search(films, Choice!);
                ValidInput = true;
            }
            catch (IOException) { Console.WriteLine(""); }

            catch (Exception) { Console.WriteLine(""); }

        } while (!ValidInput);
        ListFunctions.Display(NewList);
        return NewList;
    }

    public static List<Film> SortFilmList(List<Film> filmList)
    {
        bool ValidInput = false;
        List<Film> RecentlySortedList = null;
        do
        {
            Console.WriteLine("Hoe wilt u de filmlijst sorteren?");
            Console.WriteLine("1. Op Titel\n2. Op Genre\n3. Op duur\n4. Op Prijs\n5. regisseur\n6. Beschrijving");
            try
            {
                string? Choice = Console.ReadLine();
                string OrderBy = Choice switch
                {
                    "1" => "Name",
                    "2" => "Genre",
                    "3" => "Duration_in_minutes",
                    "4" => "Price",
                    "5" => "Director",
                    "6" => "Description",
                    _ => "Price",
                };
                Console.WriteLine("1. Oplopend\n2. Afnemend");
                string? SortBy = Console.ReadLine();

                bool Order = false;
                if (SortBy == "1") { Order = false; }
                else if (SortBy == "2") { Order = true; }
                RecentlySortedList = ListFunctions.SortList(filmList, OrderBy!, Order);
                ListFunctions.Display(RecentlySortedList);
                ValidInput = true;
            }
            catch (IOException) { Console.WriteLine(""); }
            catch (Exception) { Console.WriteLine(""); }
        } while (!ValidInput);
        return RecentlySortedList;


    }

    public static Film? Choose(List<Film> filmList)
    {
        bool ValidInput = false;
        Film? ChosenFilm = null;
        do
        {
            Console.WriteLine("\n1.Zoek een film   2.Sorteer de lijst   3.Kies een film   4.Terug naar het menu");
            try
            {
                string? Choice = Console.ReadLine();
                switch (Choice)
                {
                    case "1":
                        filmList = SearchForFilm(filmList);
                        ValidInput = false;
                        break;
                    case "2":
                        filmList = SortFilmList(filmList);
                        ValidInput = false;
                        break;
                    case "3":
                        ChosenFilm = ListFunctions.ChooseFilm(filmList);
                        // ShowMenu();
                        ValidInput = true;
                        break;
                    case "4":
                        ShowMenu();
                        break;
                    default:
                        break;
                }
            }
            catch (IOException) { Console.WriteLine(""); }
            catch (Exception) { Console.WriteLine(""); }
        } while (!ValidInput);
        return ChosenFilm;
    }

}