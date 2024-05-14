using Newtonsoft.Json;
namespace Project_B;
static class MainMenu

{
<<<<<<< Updated upstream
    private static List<Film> films = JsonReader.ReadFilmJson();
    
=======
    public static List<Film> films = JsonReader.ReadFilmJson();
    public static List<Film> SortedFilm = ListFunctions.SortList(films, "Price");

    public static Accounts account = new() { Email = "NoEmail", Age = 99 };
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

>>>>>>> Stashed changes
    public static void ShowMenu()
    {
        bool ValidInput;
        string? Choice ;   
        do
        {
            Console.WriteLine("1. Films bekijken\n2. Reserveren\n3. inloggen/Account maken\n4. Menu bioscoop restaurant bekijken");
            try
            {
                Choice = Console.ReadLine();
                ValidInput = true;
                switch (Choice)
                {
                    case "1":
                    Console.WriteLine("===De film lijst wordt geopend..===");
                    ListFunctions.Display(ListFunctions.SortList(films, "Price"));
                    ValidInput = true;
                    break;
                    case "2":
                    Console.WriteLine("===Reserveren===");
                    // Reserveren();
                    ValidInput = true;
                    break;
                    case "3":
                    Console.WriteLine("===Accounts===");
                    AccountMenuQ.Choose();
                    ValidInput = true;
                    break;
                    case "4":
                    Console.WriteLine("===Resturant menu===");
                    ValidInput = true;
                    // RestaurantMenu();
                    break;
                    default:
                    Console.WriteLine("Kies tussen 1-4!");
                    ValidInput = false;
                    break;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Invalid input! {e.Message}");
                ValidInput = false;
            }

            catch (Exception e)
            {
                Console.WriteLine($"Invalid input! {e.Message}");
                ValidInput = false;
            }

        }while(!ValidInput);


    }
}