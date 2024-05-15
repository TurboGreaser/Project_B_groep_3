namespace Project_B;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

// public static class LoginAccount
// {
//     public static string jsonfilepath = "Accounts.json";
//     public static bool IsLoggedIn { get; private set; } = false; // boolean om bij te houden of de gebruiker is ingelogd

//     public static Accounts Login(string email, string wachtwoord)
//     {
//         string text = File.ReadAllText(jsonfilepath);
//         var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

//         foreach(var user in useraccounts)
//         {
//             if (user.Password == Stringcode.Base64Encode(wachtwoord) && user.Email == email)
//             {
//                 IsLoggedIn = true; // Gebruiker is ingelogd
//                 return user;
//             }
//         }
//         Console.WriteLine("Er is geen account in het systeem met die email en wachtwoord");
//         return null;
//     }

//     public static void Logout()
//     {
//         IsLoggedIn = false; // Gebruiker uitgelogd
//     }
// }



public static class LoginAccount
{
    private static string jsonFilePath = "Accounts.json";
    public static bool IsLoggedIn { get; private set; } = false;
    public static Accounts CurrentUser { get; private set; } = null;

    public static Accounts Login(string email, string password)
    {
        string text = File.ReadAllText(jsonFilePath);
        var userAccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        foreach (var user in userAccounts)
        {
            if (user.Password == Stringcode.Base64Encode(password) && user.Email == email)
            {
                IsLoggedIn = true; // Hier wordt IsLoggedIn ingesteld op true
                CurrentUser = user;
                return user;
            }
        }
        Console.WriteLine("Er is geen account in het systeem met die e-mail en wachtwoord.");
        return null;
    }

    public static void Logout()
    {
        IsLoggedIn = false;
        CurrentUser = null;
    }
}

