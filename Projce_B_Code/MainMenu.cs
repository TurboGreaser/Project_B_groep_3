using Newtonsoft.Json;
namespace Project_B;
static class MainMenu

{
    private static List<Film> films = JsonReader.ReadFilmJson();


    public static void ShowMenu()
    {
        bool ValidInput;
        string? Choice;
        Accounts account = new Accounts()
        {
            Age = 17
        };
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
                        MainFunctions.MakeNewReservation(account);
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

        } while (!ValidInput);


    }
}