namespace Project_B;
using Newtonsoft.Json;
public static class LoginAccount
{
    public static string jsonfilepath = "Accounts.json";
    public static Accounts Login(string email, string wachtwoord)
    {
        string text = File.ReadAllText(jsonfilepath);
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        foreach(var user in useraccounts)
        {
            if (user.Password == Stringcode.Base64Encode(wachtwoord) && user.Email == email)
            {
                return user;
                //Dit returned het hele Account object.
            }
            else
            {
                Console.WriteLine("Er is geen account in het systeem met die email en wachtwoord");
                return null;
            }
        }
        return null;
    }
}