using Newtonsoft.Json;
namespace Project_B;
static class MainMenu

{
    public static List<Film> films = JsonReader.ReadFilmJson();
    public static List<Film> SortedFilm = ListFunctions.SortList(films, "Price");

    public static Accounts account = new() { Email = "NoEmail", Age = 999 };
    public static Film ShowFilmList(List<Film> SortedFilm)
    {
        int IndexOfCurrentOption = 0;
        while (true)
        {
            Console.Clear();
            foreach (Film f in SortedFilm)
            {
                int index = SortedFilm.IndexOf(f);
                if (index == IndexOfCurrentOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{index + 1}. {f.CompactInfo()}   <-- \n");

                }
                else
                {
                    Console.WriteLine($"{index + 1}. {f.CompactInfo()}");
                }

                Console.ResetColor();
            }
            Console.WriteLine("\n\nDruk op 'S' om te sorteren       Druk op 'Z' om te zoeken       Druk op 'Esc' om terug naat het menu te gaan");
            ConsoleKeyInfo KeyInput = Console.ReadKey(true);
            switch (KeyInput.Key)
            {
                case ConsoleKey.UpArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == 0) ? SortedFilm.Count - 1 : IndexOfCurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == SortedFilm.Count - 1) ? 0 : IndexOfCurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    return films[IndexOfCurrentOption];


                case ConsoleKey.S:
                    List<Film> Sorted_List = SortFilmList(SortedFilm);
                    Film Chosen_Film = ShowFilmList(Sorted_List);
                    return Chosen_Film;
                case ConsoleKey.Z:
                    List<Film> Filtered_List = SearchForFilm(films);
                    return ShowFilmList(Filtered_List);
                case ConsoleKey.Escape:
                    return null;
            }
        }
    }

    public static void ShowMenu()
    {
        int IndexOfCurrentOption = 0;
        while (true)
        {
            string[] MenuOptions;
            if (account.Email != "NoEmail")
            {
                MenuOptions = ["1. Reserveren", "2. Account bekijken", "3. Restaurant menu", "4. Verlaat programma"];
            }
            else
            {
                MenuOptions = ["1. Reserveren", "2. Aanmelden", "3. Restaurant menu", "4. Verlaat programma"];
            }
            
            Console.Clear();
            if (account.Email == "NoEmail")
            {
                Console.WriteLine($"je bent niet ingelogd");
            }
            else
            {
                Console.WriteLine($"Je bent ingeloged als {account.Username} met email: ({account.Email})");
            }
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
                    IndexOfCurrentOption = (IndexOfCurrentOption == 0) ? MenuOptions.Length - 1 : IndexOfCurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == MenuOptions.Length - 1) ? 0 : IndexOfCurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    if (IndexOfCurrentOption == MenuOptions.Length - 1)
                    {
                        Console.WriteLine("Het programma wordt gesloten");
                        return;
                    }
                    string Choice = MenuOptions[IndexOfCurrentOption];
                    if (Choice == MenuOptions[0])
                    {
                        MainFunctions.MakeNewReservation(account);
                        break;
                    }
                    else if (Choice == MenuOptions[1])
                    {
                        if (account.Email != "NoEmail")
                        {
                            // Toon accountgegevens
                            Console.WriteLine("==Account bekijken==");
                            AccountBekijken.View();
                        }
                        else
                        {
                            // Aanmelden
                            Console.WriteLine("==Aanmelden==");
                            Accounts accountToLoginWith = AccountMenuQ.Choose();
                            if (accountToLoginWith != null)
                            {
                                account = accountToLoginWith;
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("==Restaurant menu==");
                        MenuStore.PrintMenu();
                        break;
                    }
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
                NewList = ListFunctions.Search(filmList, Choice!);
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
        string[] MenuOptions = ["1. Op naam", "2. Op genre", "3. Op duur", "4. Op prijs", "5. Op regisseur", "6. Op beschrijving"];
        int IndexOfCurrentOption = 0;
        Console.WriteLine("Hoe wilt u de list sorteren");
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

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == 0) ? MenuOptions.Length - 1 : IndexOfCurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    IndexOfCurrentOption = (IndexOfCurrentOption == MenuOptions.Length - 1) ? 0 : IndexOfCurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    string Choice = MenuOptions[IndexOfCurrentOption];
                    bool asc = Asc();
                    return ListFunctions.SortList(filmList, Choice, asc);
            }

        }
    }

    public static bool Asc()
    {
        string[] MenuOptions = ["1. Aflopend", "2. Oplopend"];
        int CurrentOption = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Aflopend of Oplopend?");
            for (int i = 0; i < MenuOptions.Length; i++)
            {
                if (i == CurrentOption)
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

            ConsoleKeyInfo KeyInfo = Console.ReadKey(true);
            switch (KeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    CurrentOption = (CurrentOption == 0) ? 1 : 0;
                    break;

                case ConsoleKey.DownArrow:
                    CurrentOption = (CurrentOption == 1) ? 0 : 1;
                    break;
                case ConsoleKey.Enter:
                    return CurrentOption == 1;
            }
        }
    }
    // public static List<Film> SortFilmList(List<Film> filmList)
    // {
    //     bool ValidInput = false;
    //     List<Film> RecentlySortedList = null;
    //     do
    //     {
    //         Console.WriteLine("Hoe wilt u de filmlijst sorteren?");
    //         Console.WriteLine("1. Op Titel\n2. Op Genre\n3. Op duur\n4. Op Prijs\n5. regisseur\n6. Beschrijving");
    //         try
    //         {
    //             string? Choice = Console.ReadLine();
    //             string OrderBy = Choice switch
    //             {
    //                 "1" => "Name",
    //                 "2" => "Genre",
    //                 "3" => "Duration_in_minutes",
    //                 "4" => "Price",
    //                 "5" => "Director",
    //                 "6" => "Description",
    //                 _ => "Price",
    //             };
    //             Console.WriteLine("1. Oplopend\n2. Afnemend");
    //             string? SortBy = Console.ReadLine();

    //             bool Order = false;
    //             if (SortBy == "1") { Order = false; }
    //             else if (SortBy == "2") { Order = true; }
    //             RecentlySortedList = ListFunctions.SortList(filmList, OrderBy!, Order);
    //             ListFunctions.Display(RecentlySortedList);
    //             ValidInput = true;
    //         }
    //         catch (IOException) { Console.WriteLine(""); }
    //         catch (Exception) { Console.WriteLine(""); }
    //     } while (!ValidInput);
    //     return RecentlySortedList;


    // }

    // public static Film? Choose(List<Film> filmList)
    // {
    //     bool ValidInput = false;
    //     Film? ChosenFilm = null;
    //     do
    //     {
    //         Console.WriteLine("\n1.Zoek een film   2.Sorteer de lijst   3.Kies een film   4.Terug naar het menu");
    //         try
    //         {
    //             string? Choice = Console.ReadLine();
    //             switch (Choice)
    //             {
    //                 case "1":
    //                     filmList = SearchForFilm(filmList);
    //                     ValidInput = false;
    //                     break;
    //                 case "2":
    //                     filmList = SortFilmList(filmList);
    //                     ValidInput = false;
    //                     break;
    //                 case "3":
    //                     ChosenFilm = ListFunctions.ChooseFilm(filmList);
    //                     // ShowMenu();
    //                     ValidInput = true;
    //                     break;
    //                 case "4":
    //                     ShowMenu();
    //                     break;
    //                 default:
    //                     break;
    //             }
    //         }
    //         catch (IOException) { Console.WriteLine(""); }
    //         catch (Exception) { Console.WriteLine(""); }
    //     } while (!ValidInput);
    //     return ChosenFilm;
    // }
}