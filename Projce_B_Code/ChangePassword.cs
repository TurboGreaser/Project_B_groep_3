using Newtonsoft.Json;
namespace Project_B;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class ChangePassword
{
    public static string jsonfilepath = "Accounts.json";

    public static void ChangePass(string email)
    {
        string text = File.ReadAllText(jsonfilepath);
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        Console.WriteLine("Wat is je oude wachtwoord?");
        string oldpass = Console.ReadLine();

        foreach (var user in useraccounts)
        {
            if (user.Password == Stringcode.Base64Encode(oldpass) && user.Email == email) // werkt niet bij al gemaakte account want die wachtwoorden zijn niet gehashed in de json, als je wel wilt testen haalt dan "Stringcode.Base64Encode" weg
            {
                Console.WriteLine("Enter je nieuwe wachtwoord (moet meer dan 3 karakters hebben):");
                string firstentpassword = Console.ReadLine();
                Console.WriteLine("Bevestig je nieuwe wachtwoord:");
                string secondentpassword = Console.ReadLine();

                if (firstentpassword == secondentpassword && firstentpassword.Length > 3)
                {
                    
                    user.Password = Stringcode.Base64Encode(firstentpassword);
                    string json = JsonConvert.SerializeObject(useraccounts, Formatting.Indented);
                    File.WriteAllText(jsonfilepath, json);
                    Console.WriteLine("Wachtwoord succesvol gewijzigd!");
                    return; 
                }
                else
                {
                    if (firstentpassword.Length <= 3)
                    {
                        Console.WriteLine("Je wachtwoord moet meer dan 3 karakters zijn!");
                    }
                    else
                    {
                        Console.WriteLine("De ingevoerde wachtwoorden komen niet overeen!");
                    }
                    return; 
                }
            }
        }

        Console.WriteLine("Er is geen account in het systeem met dat e-mailadres en wachtwoord.");
    }
}