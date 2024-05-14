namespace Project_B;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public static class AddAccount
{
    public static string jsonfilepath = "Accounts.json";
    public static string UserName;
    public static string Email;
    public static int Age;
    public static string Password;

    private static string text = File.ReadAllText(jsonfilepath);
    private static List<Accounts> useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

    public static (string, string) MakeAccount()
    {
        Console.WriteLine("De terms en conditions zijn als volgt:\nDoor gebruik te maken van deze dienst gaat u akkoord met deze Algemene Voorwaarden.\nIndien u niet akkoord gaat met deze voorwaarden, dient u geen gebruik te maken van de dienst.");
        Console.WriteLine("Druk op een knop om verder te gaan");
        Console.ReadKey(true);
        Console.WriteLine("Acepteer je de terms en conditions? ");

        string[] JaOfNee = ["Ja", "Nee"];
        int CurrentOption = 0;
        while (true)
        {
            Console.Clear();
            for (int i = 0; i < 2; i++)
            {
                if (i == CurrentOption)
                {
                    Console.Write("--> ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.Write("    ");
                }
                Console.WriteLine(JaOfNee[i]);
                Console.ResetColor();
            }
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    CurrentOption = (CurrentOption == 0) ? 1 : 0;
                    break;

                case ConsoleKey.DownArrow:
                    CurrentOption = (CurrentOption == 0) ? 1 : 0;
                    break;

                case ConsoleKey.Enter:
                    if (CurrentOption == 0)
                    {
                        // function for making account
                        return Gegevens();
                    }
                    else
                    {
                        return ("", "");
                    }
            }
        }
    }

    public static (string, bool) GetUserName()
    {
        Console.Clear();
        Console.WriteLine("Enter je gewilde Gebruikersnaam:");
        string UserName = Console.ReadLine();
        foreach (var user in useraccounts)
        {
            if (UserName.ToUpper() == user.Username.ToUpper())
            {
                Console.WriteLine("De naam is al in gebruik, neem een andere naam");
                Console.WriteLine("\nDruk Enter om verder te gaan");
                Console.ReadLine();
                return (UserName, false);
            }
        }
        return (UserName, true);

    }

    public static (string recipient, string domain, string topLevelDomain) ParseEmail(string emailAddress)
    {
        string pattern = @"^.+@.+..+$";
        Regex regex = new Regex(pattern);


        if (regex.IsMatch(emailAddress))
        {
            string[] splitEmail = emailAddress.Split('@');

            string recipient = splitEmail.First().Trim();

            string domain = splitEmail.Last().Trim();

            string topLevelDomain = domain.Split('.').Last();


            return (recipient, domain, topLevelDomain);
        }
        return (null, null, null);
    }

    public static (string, bool) GetEmail()
    {
        Console.Clear();
        Console.WriteLine("Enter je Email: ");
        Console.WriteLine("De Email moet in deze format zijn: example@email.com");
        string Email = Console.ReadLine();
        foreach (var account in useraccounts)
        {
            if (Email == account.Email)
            {
                Console.WriteLine("De email is al in gebruik, weet je zeker dat je de correcte email hebt ingevoerd?");
                Console.WriteLine("\nDruk Enter om verder te gaan");
                Console.ReadLine();
                return ("false", false);
            }

        }
        var ParsedEmail = ParseEmail(Email);
        if (ParsedEmail == (null, null, null))
        {
            ParsedEmail = ParseEmail(Email);
            Console.WriteLine("De Email moet in deze format zijn: example@email.com");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return ("false", false);
        }
        return (Email, true);


    }

    public static int GetAge()
    {
        Console.Clear();
        Console.WriteLine("Enter je leeftijd");
        int Age = Convert.ToInt32(Console.ReadLine());
        if (Age < 0)
        {
            Console.WriteLine("Leeftijd mag niet kleiner dan 0 zijn");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return -1;
        }
        if (Age > 111)
        {
            Console.WriteLine("Leeftijd mag niet groter dan 111 zijn");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return -1;
        }
        return Age;

    }

    public static (string, string) Gegevens()
    {
        (string, bool) UserNameTuple = GetUserName();
        //Kijk of de UserName all bestaat in de Json, als het nog niet bestaat in de Json dan mag de gebruiker deze UserName hebben anders niet.
        while (!UserNameTuple.Item2)
        {
            UserNameTuple = GetUserName();
        }
        string UserName = UserNameTuple.Item1;

        var EmailTuple = GetEmail();
        while (!EmailTuple.Item2)
        {
            EmailTuple = GetEmail();
        }
        Email = EmailTuple.Item1;
        //nu age
        int Age = GetAge();
        while (Age == -1)
        {
            Age = GetAge();
        }
        string Password = Wachtwoord();
        while (Password == "0")
        {
            Password = Wachtwoord();
        }
        string rawPass = Password;
        Password = Stringcode.Base64Encode(Password);

        //Stuur deze Info naar de Json class waarin het naar json wordt gestuurd
        AddAccountToJson addnewacc = new(UserName, Email, Age, Password);
        addnewacc.AddToJson();
        return (Email, rawPass);

    }

    public static string Wachtwoord()
    {
        Console.Clear();
        Console.WriteLine("Enter je wachtwoord");
        Console.WriteLine("Het wachtwoord moet tussen 8 en 20 karakters hebben\nen minimaal een speciale karakter");
        string Password1 = Console.ReadLine();
        string pattern = @"^(?=.*[!@#$%^&*()-=_+[\]{};:'""|<>,./?]).{8,20}$";
        if (!Regex.IsMatch(Password1, pattern))
        {
            Console.WriteLine("Het wachtwoord moet tussen 8 en 20 karakters hebben\nen minimaal een speciale karakter");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return "0";
        }
        Console.WriteLine("Voer je wachtwoord opnieuw in.");
        string Password2 = Console.ReadLine();
        if (Password1 != Password2)
        {
            Console.WriteLine("Wachtwoorden zijn niet hetzelfde");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return "0";
        }
        return Password1;
    }
    //         else
    //         {
    //             Console.WriteLine("You did not accept the Terms and Conditions and will now be put back to the main menu");
    //             //Gooi uit de class
    //         }
    //     }
}
