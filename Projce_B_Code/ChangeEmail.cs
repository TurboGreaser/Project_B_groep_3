using Newtonsoft.Json;
namespace Project_B;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class ChangeEmail
{
    public static string jsonfilepath = "Accounts.json";

    public static void ChangeMail()
    {
        string text = File.ReadAllText(jsonfilepath);
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        Console.WriteLine("Wat is je oude e-mailadres?\n");
        string email = Console.ReadLine();

        Console.WriteLine("Wat is je wachtwoord?\n");
        string oldpass = Console.ReadLine();

        foreach (var user in useraccounts)
        {
            if (user.Password == Stringcode.Base64Encode(oldpass) && user.Email == email) // werkt niet bij al gemaakte account want die wachtwoorden zijn niet gehashed in de json, als je wel wilt testen haalt dan "Stringcode.Base64Encode" weg
            {

                var new_Mail = AddAccount.GetEmail();
                while (new_Mail == (null, false))
                {
                    new_Mail = AddAccount.GetEmail();
                }
                string user_mail = new_Mail.Item1;
                bool user_Bool = new_Mail.Item2;

                user.Email = user_mail;
                string json = JsonConvert.SerializeObject(useraccounts, Formatting.Indented);
                File.WriteAllText(jsonfilepath, json);
                Console.WriteLine("Email succesvol gewijzigd!");

                return; 
                

                
            }
        }

        Console.WriteLine("Er is geen account in het systeem met het e-mailadres en wachtwoord.");
    }
}