using Newtonsoft.Json;
namespace Project_B;
public static class MainMenu

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
            FormattedOutput.Display_FilmList();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\nDruk op 'S' om de volgorde van films te veranderen   Druk op 'Z' om te zoeken   Druk op 'Esc' om terug naar het menu te gaan");
            Console.WriteLine("kies de film om te reserveren/ de beschrijving te zien\n\n");
            Console.ResetColor();
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
                    Film ChosenFilm = SortedFilm[IndexOfCurrentOption];
                    if (ShowDescription(ChosenFilm) is null)
                    {
                        break;
                    }
                    else
                    {
                        return ChosenFilm;
                    }

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
                MenuOptions = ["1. Hier kunt u Reserveren/ films bekijken", "2. Uw account bekijken", "3. Bekijk hier het Restaurant menu", "4. Klik hier om het Programma te verlaten!"];
            }
            else
            {
                MenuOptions = ["1. Hier kunt u Reserveren/ films bekijken", "2. Hier kunt u Registeren/Inloggen/Wachtwoord resetten en Doorgaan zonder account!", "3. Bekijk hier het Restaurant menu", "4. Klik hier om het Programma te verlaten!"];
            }

            Console.Clear();
            FormattedOutput.Display_Title();
            FormattedOutput.Display_Todays_Films();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Gebruik de pijltjes om naar beneden/ boven te gaan.");
            Console.WriteLine("Gebruik 'Enter' om te kiezen");
            Console.ResetColor();
            if (account.Email == "NoEmail")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"U bent niet ingelogd");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"U bent ingeloged als {account.Username} met email: ({account.Email})");
                Console.ResetColor();
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
            Console.WriteLine("Voer de titel van de film in:");
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
        string[] MenuOptions = ["1. Op naam", "2. Op genre", "3. Op duur", "4. Op prijs", "5. Op regisseur"];
        int IndexOfCurrentOption = 0;
        Console.WriteLine("Hoe wilt u de lijst sorteren?");
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

    public static Film? ShowDescription(Film ChosenFilm)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{ChosenFilm.CompactInfo()}\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Druk op 'B' om de beschrijving van deze film te lezen");
            Console.WriteLine("Druk op 'Enter' om deze film te kiezen");
            Console.WriteLine("Druk op 'Esc' om terug naar de filmlijst te gaan");
            Console.ResetColor();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape:
                    return null;
                case ConsoleKey.Enter:
                    return ChosenFilm;
                case ConsoleKey.B:
                    Console.WriteLine($"\nFilm: {ChosenFilm.Name}\nBeschrijving: {ChosenFilm.Description}");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nDruk op 'Enter' om de film te kiezen");
                    Console.WriteLine("Druk op een andere knop om terug naar de filmlijst te gaan");
                    Console.ResetColor();
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        return ChosenFilm;
                    }
                    else
                    {
                        return null;
                    }
                default:
                    break;

            }

        }
    }
    
}