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
            Console.WriteLine("1. Films bekijken\n2. Reserveren\n3. Account maken\n4. Menu bioscoop restaurant bekijken");
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
                    Console.WriteLine("===Account maken===");
                    // MakeAccount();
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

    public static void Choose(List<Film> filmList)
    {
        bool ValidInput = false;
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
                    // SortFilmList();
                    ValidInput = true;
                    break;
                    case "3":
                    Film ChosenFilm = ListFunctions.ChooseFilm(filmList);
                    ValidInput = true;
                    break;
                    case "4":
                    ShowMenu();
                    break;
                    default :
                    break;
                }
            }
            catch(IOException) {Console.WriteLine("");}
            catch(Exception){ Console.WriteLine("");}
            }while(!ValidInput);                
            
    }
}