namespace Project_B;
using Newtonsoft.Json;
public static class ForgotPassword
{
    public static string jsonfilepath = "Accounts.json";
    public static Accounts LoginWithSecondary(string email, string secondarypassword)
    {
        string text = File.ReadAllText(jsonfilepath);
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        foreach(var user in useraccounts)
        {
            if (user.SecondPassword == Stringcode.Base64Encode(secondarypassword) && user.Email == email)
            {

                Console.WriteLine("Voer nu je nieuwe wachtwoord in!\n");
                Console.WriteLine("\n");
                Console.ReadLine();
                string new_password = AddAccount.Wachtwoord();

                while (new_password == "0")
                {
                    new_password = AddAccount.Wachtwoord();
                } 

                user.Password = Stringcode.Base64Encode(new_password);
                string json = JsonConvert.SerializeObject(useraccounts, Formatting.Indented);
                File.WriteAllText(jsonfilepath, json);
                Console.WriteLine("\n");
                Console.WriteLine("\n");
                Console.WriteLine("Wachtwoord succesvol gewijzigd!");
                Console.ReadLine();
                return user;
            }

        }
        Console.WriteLine("Er is geen account in het systeem met die email en back-up wachtwoord");
        return null;
    }
    
}

