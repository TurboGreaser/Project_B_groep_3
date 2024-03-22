static class MainMenu
{
    public static void ShowMenu()
    {
        bool ValidInput = false;
        string? Choice = "";   
        do
        {
            Console.WriteLine("1. Films bekijken\n2. reserveren\n3. account maken\n4. Menu bioscoop restaurant bekijken");
            try
            {
                Choice = Console.ReadLine();
                ValidInput = true;
                switch (Choice)
                {
                    case "1":
                    Console.WriteLine("opening the list");
                    // FilmList();
                    break;
                    case "2":
                    Console.WriteLine("reserving");
                    // Reserveren();
                    break;
                    case "3":
                    Console.WriteLine("Making Account");
                    // MakeAccount();                   Voeg Deleting Account toe.
                    break;
                    case "4":
                    Console.WriteLine("Showing restaurant menu");
                    // RestaurantMenu();
                    break;
                    default:
                    Console.WriteLine("Invalid input. Choose a number between 1-4");
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