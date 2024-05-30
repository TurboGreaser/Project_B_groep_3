namespace Project_B;
using Newtonsoft.Json;

public static class RemoveAccount
{
    public static string jsonfilepath = "Accounts.json";
    public static void GetInfo()
    {
        Console.WriteLine("Om je account weg te halen moet je je email en wachtwoord invoeren");
        Console.WriteLine("Wat is je email?");
        string enteredemail = Console.ReadLine();
        Console.WriteLine("Wat is je wachtwoord?");
        string enteredpassword = Console.ReadLine();


        //Roep de removeAccount method aan
        RemoveAccountByPassAndEmail(enteredemail, enteredpassword);
    }
    public static void RemoveAccountByPassAndEmail(string email, string password)
    {
        string text = File.ReadAllText(jsonfilepath);
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        //zoekt naar de email/wachtwoord 
        Accounts usertoremove = useraccounts.Find(person => person.Email == email && person.Password == Stringcode.Base64Encode(password));

        if (usertoremove != null)
        {
            useraccounts.Remove(usertoremove);

            string jsontext = JsonConvert.SerializeObject(useraccounts, Formatting.Indented);

            File.WriteAllText(jsonfilepath, jsontext);

            Console.WriteLine("Je account is succesvol verwijderd!");
        }
        else
        {
            Console.WriteLine("Account met email en wachtwoord is niet gevonden");
        }
    }
}