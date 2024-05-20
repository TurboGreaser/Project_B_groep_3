namespace Project_B; 
using Newtonsoft.Json;
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

        bool emailFound = false;
        bool passwordCorrect = false;

        foreach (var user in useraccounts)
        {
            if (user.Email == email)
            {
                emailFound = true;
                if (user.Password == Stringcode.Base64Encode(oldpass))
                {
                    passwordCorrect = true;

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
                else
                {
                    Console.WriteLine("Het wachtwoord is onjuist. Probeer het opnieuw.\n");
                    return;
                }
            }
        }

        if (!emailFound && !passwordCorrect)
        {
            Console.WriteLine("Er is geen account in het systeem met het opgegeven e-mailadres en wachtwoord.");
        }
        else if (!passwordCorrect)
        {
            Console.WriteLine("Het wachtwoord is onjuist. Probeer het opnieuw.");
        }
        else if (!emailFound)
        {
            Console.WriteLine("Er is geen account in het systeem met het opgegeven e-mailadres.");
        }

    }
}