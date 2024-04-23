using Newtonsoft.Json;
namespace Project_B;
static class MainMenu

{
    public static List<Film> films = JsonReader.ReadFilmJson();
    
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
                    Choose(films);
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

    public static void SearchForFilm(List<Film> filmList)
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
            catch (IOException){Console.WriteLine("");}

            catch(Exception){Console.WriteLine("");}

        }while(!ValidInput);
        ListFunctions.Display(NewList);
        Choose(NewList);
    }

    public static void SortFilmList(List<Film> filmList)
    {
        bool ValidInput = false;
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
                if (SortBy == "1") {Order = false;}
                else if (SortBy == "2") {Order = true;}
                List<Film> RecentlySortedList = ListFunctions.SortList(filmList, OrderBy!, Order);
                ListFunctions.Display(RecentlySortedList);
                Choose(RecentlySortedList);
            }
            catch(IOException){Console.WriteLine("");}
            catch (Exception){Console.WriteLine("");}
        }while(!ValidInput);

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
                switch(Choice)
                {
                    case "1":
                    SearchForFilm(filmList);
                    ValidInput = true;
                    break;
                    case "2":
                    SortFilmList(filmList);
                    ValidInput = true;
                    break;
                    case "3":
                    ChosenFilm = ListFunctions.ChooseFilm(filmList);
                    ShowMenu();
                    ValidInput = true;
                    break;
                    case "4":
                    ShowMenu();
                    break;
                    default:
                    break;
                }
            }
            catch(IOException) {Console.WriteLine("");}
            catch(Exception){ Console.WriteLine("");}
            }while(!ValidInput);
        return ChosenFilm;             
    }

}