// using Newtonsoft.Json;

// namespace Project_B
// {
//     public static class AccountBekijken
//     {
//         private static string jsonFilePath = "Accounts.json";

//         public static void View()
//         {
//             // Controleer of de gebruiker is ingelogd
//             if (LoginAccount.CurrentUser != null && LoginAccount.CurrentUser.IsLoggedIn)
//             {
//                 Console.WriteLine("Wilt u de accountgegevens zien? Typ dan 1\n");
//                 Console.WriteLine("Wilt u uw reservaties zien? Typ dan 2\n");
//                 Console.WriteLine("Wilt u uw reservaties annuleren? Typ dan 3\n");
//                 Console.WriteLine("Wilt u uw favoriete films zien? Typ dan 4\n");

//                 string user_choice = Console.ReadLine();

//                 while (user_choice != "1" && user_choice != "2" && user_choice != "3" && user_choice != "4")
//                 {
//                     Console.WriteLine("Ongeldige keuze. Probeer opnieuw.\n");
//                     user_choice = Console.ReadLine();
//                 }

//                 if (user_choice == "1")
//                 {
//                     try
//                     {
//                         // Lees de inhoud van Accounts.json
//                         string text = File.ReadAllText(jsonFilePath);
//                         // Deserialiseer de JSON-gegevens naar een lijst van Accounts
//                         var userAccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

//                         // Zoek het huidige ingelogde account op basis van de e-mail
//                         var currentUser = userAccounts.FirstOrDefault(account => account.Email == LoginAccount.CurrentUser.Email);

//                         if (currentUser != null)
//                         {
//                             // Print alle informatie van het huidige ingelogde account
//                             Console.WriteLine("Accountgegevens:");
//                             Console.WriteLine($"Gebruikersnaam: {currentUser.Username}");
//                             Console.WriteLine($"E-mail: {currentUser.Email}");
//                             Console.WriteLine($"Leeftijd: {currentUser.Age}");
//                             // Voeg hier andere velden toe die je in Accounts.json hebt
//                         }
//                         else
//                         {
//                             Console.WriteLine("Huidige gebruiker niet gevonden in Accounts.json.");
//                         }
//                     }
//                     catch (Exception ex)
//                     {
//                         Console.WriteLine($"Er is een fout opgetreden bij het weergeven van accountgegevens: {ex.Message}");
//                     }
//                 }

//                 // else if (user_choice == "2")
//                 // {

//                 // }

//                 else if (user_choice == "3")
//                 {
//                     CancelReservation.InfoFromUser(LoginAccount.CurrentUser.Email);
//                     string test = Console.ReadLine();
//                 }

//                 // else if (user_choice == "4") 
//                 // {

//                 // }
            
          

//             }
//         }
//     }
// }


using Newtonsoft.Json;
namespace Project_B
{
    public static class AccountBekijken
    {
        private static string jsonFilePath = "Accounts.json";

        public static void View()
        {
            // Controleer of de gebruiker is ingelogd
            if (LoginAccount.CurrentUser != null && LoginAccount.CurrentUser.IsLoggedIn)
            {
                string[] menuOptions = {
                    "1. Wilt u de accountgegevens zien?",
                    "2. Wilt u uw reservaties zien?",
                    "3. Wilt u uw reservaties annuleren?",
                    "4. Wilt u uw favoriete films zien?"
                };

                int indexOfCurrentOption = 0;

                while (true)
                {
                    Console.Clear();

                    for (int i = 0; i < menuOptions.Length; i++)
                    {
                        if (i == indexOfCurrentOption)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("--> ");
                        }
                        else
                        {
                            Console.Write("    ");
                        }
                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();
                    }

                    ConsoleKeyInfo keyInput = Console.ReadKey(true);
                    switch (keyInput.Key)
                    {
                        case ConsoleKey.UpArrow:
                            indexOfCurrentOption = (indexOfCurrentOption == 0) ? menuOptions.Length - 1 : indexOfCurrentOption - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            indexOfCurrentOption = (indexOfCurrentOption == menuOptions.Length - 1) ? 0 : indexOfCurrentOption + 1;
                            break;
                        case ConsoleKey.Enter:
                            HandleMenuOption(indexOfCurrentOption + 1);
                            return;
                    }
                }
            }
            else
            {
                Console.WriteLine("U bent niet ingelogd.");
            }
        }

        private static void HandleMenuOption(int option)
        {
            switch (option)
            {
                case 1:
                    ShowAccountDetails();
                    break;
                case 2:

                    break;
                case 3:
                    CancelReservation.InfoFromUser(LoginAccount.CurrentUser.Email);
                    string test = Console.ReadLine();
                    break;
                case 4:
                    
                    break;
                default:
                    Console.WriteLine("Ongeldige optie.");
                    break;
            }
        }

        private static void ShowAccountDetails()
        {
            try
            {
 
                string text = File.ReadAllText(jsonFilePath);
      
                var userAccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);


                var currentUser = userAccounts.FirstOrDefault(account => account.Email == LoginAccount.CurrentUser.Email);

                if (currentUser != null)
                {
                   
                    Console.WriteLine("Accountgegevens:");
                    Console.WriteLine($"Gebruikersnaam: {currentUser.Username}");
                    Console.WriteLine($"E-mail: {currentUser.Email}");
                    Console.WriteLine($"Leeftijd: {currentUser.Age}");
                 
                }
                else
                {
                    Console.WriteLine("Huidige gebruiker niet gevonden in Accounts.json.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Er is een fout opgetreden bij het weergeven van accountgegevens: {ex.Message}");
            }
        }
    }
}
