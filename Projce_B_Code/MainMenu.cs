static class MainMenu
{
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
                    Console.WriteLine("===Lijst wordt geopend===");
                    // FilmList();
                    ValidInput = true;
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
}