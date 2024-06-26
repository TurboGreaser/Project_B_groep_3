﻿namespace Project_B;
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
    public static string SecondPassword;
    public static string SecurityQuestion;

    private static string text = File.ReadAllText(jsonfilepath);
    private static List<Accounts> useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

    public static (string, string) MakeAccount()
    {
        Console.WriteLine("De Terms en Conditions zijn als volgt:\nDoor gebruik te maken van deze dienst gaat u akkoord met deze Algemene Voorwaarden.\nIndien u niet akkoord gaat met deze voorwaarden, dient u geen gebruik te maken van de dienst.");
        Console.WriteLine("Druk op een knop om verder te gaan");
        Console.ReadKey(true);
        Console.WriteLine("Accepteert u de Terms en Conditions? ");

        string[] JaOfNee = new string[] { "Ja", "Nee" };
        int CurrentOption = 0;




        while (true)
        {
            Console.Clear();
            Console.WriteLine("De Terms en Conditions zijn als volgt:\nDoor gebruik te maken van deze dienst gaat u akkoord met deze Algemene Voorwaarden.\nIndien u niet akkoord gaat met deze voorwaarden, dient u geen gebruik te maken van de dienst.");
            // Console.WriteLine("Druk op een knop om verder te gaan");
            Console.WriteLine("\nAccepteert u de Terms en Conditions? ");
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
        Console.WriteLine("Vul uw gewilde Gebruikersnaam in:");
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

    public static bool ParseEmail(string emailAddress)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);

        if (regex.IsMatch(emailAddress))
        {
            return true;
        }
        return false;
    }

    public static (string, bool) GetEmail()
    {
        Console.Clear();
        Console.WriteLine("Vul uw Email in: ");
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
        if (!ParsedEmail)
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
        try
        {
            Console.WriteLine("Vul je leeftijd in");
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
        catch (Exception ex)
        {
            Console.WriteLine("Leeftijd moet een nummer zijn/mag niet leeg gelaten worden");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return -1;
        }

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

        var backupInfo = BackupWachtwoord();
        while (backupInfo == (null, null))
        {
            backupInfo = BackupWachtwoord();
        }
        string securityQuestion = Stringcode.Base64Encode(backupInfo.question);
        string securityAnswer = Stringcode.Base64Encode(backupInfo.answer);

        //Stuur deze Info naar de Json class waarin het naar json wordt gestuurd
        AddAccountToJson addnewacc = new(UserName, Email, Age, Password, securityAnswer, securityQuestion);
        addnewacc.AddToJson();
        return (Email, rawPass);

    }

    public static string Wachtwoord()
    {
        Console.Clear();
        Console.WriteLine("Voer je wachtwoord in");
        Console.WriteLine("Het wachtwoord moet tussen 8 en 20 karakters hebben\nen minimaal een speciale karakter");
        string Password1 = MiscFunctions.ReadLinePasswordHider();
        string pattern = @"^(?=.*[!@#$%^&*()-=_+[\]{};:'""|<>,./?]).{8,20}$";
        if (!Regex.IsMatch(Password1, pattern))
        {
            Console.WriteLine("Het wachtwoord moet tussen 8 en 20 karakters hebben\nen minimaal een speciale karakter");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return "0";
        }
        Console.WriteLine("Voer je wachtwoord opnieuw in.");
        string Password2 = MiscFunctions.ReadLinePasswordHider();
        if (Password1 != Password2)
        {
            Console.WriteLine("Wachtwoorden zijn niet hetzelfde");
            Console.WriteLine("\nDruk Enter om verder te gaan");
            Console.ReadLine();
            return "0";
        }
        return Password1;
    }

    public static (string question, string answer) BackupWachtwoord()
    {
        Console.Clear();

        string[] questions =
        {
            "Wat is je levensmotto?",
            "Wat is de naam van je beste jeugd vriend?",
            "Wat is je favoriete gerecht?"
        };

        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Kies een beveilingsvraag voor je back-up wachtwoord:\n");

            for (int i = 0; i < questions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"--> {questions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"    {questions[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : questions.Length - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex < questions.Length - 1) ? selectedIndex + 1 : 0;
            }

        } while (key != ConsoleKey.Enter);

        string securityQuestion = questions[selectedIndex];

        Console.Clear();
        Console.WriteLine($"Voer je antwoord in op de vraag: {securityQuestion}\n");
        string userAnswer = Console.ReadLine();

        return (securityQuestion, userAnswer);
    }

    // public static (string question, string answer) BackupWachtwoord1()
    // {
    //     Console.Clear();
    //     Console.WriteLine("Kies een beveilingsvraag voor je back-up wachtwoord:\n");
    //     Console.WriteLine("1. Wat is je levensmotto?");
    //     Console.WriteLine("2. Wat is de naam van je beste jeugd vriend?");
    //     Console.WriteLine("3. Wat is je favoriete gerecht?");

    //     int userInput;
    //     if (!int.TryParse(Console.ReadLine(), out userInput) || userInput < 1 || userInput > 3)
    //     {
    //         Console.WriteLine("Verkeerde input. Voer een getal in van 1 tot en met 3.");
    //         Console.WriteLine("\nDruk Enter om verder te gaan.");
    //         Console.ReadLine();
    //         return (string.Empty, string.Empty);
    //     }

    //     string securityQuestion;
    //     switch (userInput)
    //     {
    //         case 1:
    //             securityQuestion = "Wat is je levensmotto?";
    //             break;
    //         case 2:
    //             securityQuestion = "Wat is de naam van je beste jeugd vriend?";
    //             break;
    //         case 3:
    //             securityQuestion = "Wat is je favoriete gerecht?: ";
    //             break;
    //         default:
    //             securityQuestion = "";
    //             break;
    //     }

    //     Console.WriteLine($"Voer je antwoord in op de vraag: {securityQuestion}\n");
    //     string User_Answer = Console.ReadLine();
    //     return (securityQuestion, User_Answer);
    // }




    //         else
    //         {
    //             Console.WriteLine("You did not accept the Terms and Conditions and will now be put back to the main menu");
    //             //Gooi uit de class
    //         }
    //     }

}
