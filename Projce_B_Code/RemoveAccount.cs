using Newtonsoft.Json;

public static class RemoveAccount
{
    public static string jsonfilepath = "Accounts.json";
    public static void GetInfo()
    {
        Console.WriteLine("To remove your account you must enter Your accounts Email and Password");
        Console.WriteLine("What is your email?");
        string enteredemail = Console.ReadLine();
        Console.WriteLine("What is your password?");
        string enteredpassword = Console.ReadLine();


        //Roep de removeAccount method aan
        RemoveAccountByPassAndEmail(enteredemail, enteredpassword);
    }
    public static void RemoveAccountByPassAndEmail(string email, string password)
    {
        
        
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        //zoekt naar de email/wachtwoord 
        Accounts usertoremove = useraccounts.Find(person => person.Email == email && person.Password == password);

        if (usertoremove != null)
        {
            useraccounts.Remove(usertoremove);

            string jsontext = JsonConvert.SerializeObject(useraccounts, Formatting.Indented);

            File.WriteAllText(jsonfilepath, jsontext);

            Console.WriteLine("Your account has been removed from memory succesfully");
        }
        else
        {
            Console.WriteLine("Account with the entered Email and Password was not found");
        }
    }
}