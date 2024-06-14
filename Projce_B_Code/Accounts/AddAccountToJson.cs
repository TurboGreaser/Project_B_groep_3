namespace Project_B;
using Newtonsoft.Json;

public class AddAccountToJson
{
    public string jsonfilepath = "Accounts.json";
    public string UserName;
    public string Email;
    public int Age;
    public string Password;
    public string SecondPassword;
    public string SecurityQuestion;

    public AddAccountToJson(string username, string email, int age, string password, string secondpassword, string securityQuestion)
    {
        UserName = username;
        Email = email;
        Age = age;
        Password = password;
        SecondPassword = secondpassword;
        SecurityQuestion = securityQuestion;

    }

    public void AddToJson()
    {
        string jsonttext = File.ReadAllText(jsonfilepath);
        List<Accounts> accounts = JsonConvert.DeserializeObject<List<Accounts>>(jsonttext);

        Accounts newAccount = new()
        {
            Username = UserName,
            Email = Email,
            Age = Age,
            Password = Password,
            SecondPassword = SecondPassword,
            SecurityQuestion = SecurityQuestion,
            savedInformationlist = new List<Dictionary<string, string>>()
        };

        accounts.Add(newAccount);

        string jsontext = JsonConvert.SerializeObject(accounts, Formatting.Indented);

        File.WriteAllText(jsonfilepath, jsontext);
    }
}