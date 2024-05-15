using Newtonsoft.Json;

namespace Project_B
{
    public static class AccountBekijken
    {
        private static string jsonFilePath = "Accounts.json";

        public static void View()
        {
            // Controleer of de gebruiker is ingelogd
            if (LoginAccount.IsLoggedIn && LoginAccount.CurrentUser != null)
            {
                try
                {
                    // Lees de inhoud van Accounts.json
                    string text = File.ReadAllText(jsonFilePath);
                    // Deserialiseer de JSON-gegevens naar een lijst van Accounts
                    var userAccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);
                    
                    // Zoek het huidige ingelogde account op basis van de e-mail
                    var currentUser = userAccounts.FirstOrDefault(account => account.Email == LoginAccount.CurrentUser.Email);
                    
                    if (currentUser != null)
                    {
                        // Print alle informatie van het huidige ingelogde account
                        Console.WriteLine("Accountgegevens:");
                        Console.WriteLine($"Gebruikersnaam: {currentUser.Username}");
                        Console.WriteLine($"E-mail: {currentUser.Email}");
                        Console.WriteLine($"Leeftijd: {currentUser.Age}");
                        // Voeg hier andere velden toe die je in Accounts.json hebt
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
            else
            {
                Console.WriteLine("Er is geen gebruiker ingelogd.");
            }
        }
    }
}
