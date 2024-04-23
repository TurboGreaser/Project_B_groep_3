namespace Project_B;
using Newtonsoft.Json;

public class AddFavMovie
{
    public readonly string Username;
    public readonly string Password;
    private Dictionary<string, string> Favoriete_Film = new(); //Dictionary 

    private const string JsonFilePath = "Accounts.json";


    public AddFavMovie(string username, string password) //De constructor wordt aangeroepen met de acc username/ww
    {
        Username = username;
        Password = password;
    }
    // {"Titel" : "_test_", "Director" : "_test_"} zo ziet de dict eruit 


    public void AddFMovieToJson(string movietitel, string filmdirector)
    {
        string jsontext = File.ReadAllText(JsonFilePath);
        List<Accounts> accounts = JsonConvert.DeserializeObject<List<Accounts>>(jsontext);

        foreach (var acc in accounts)
        {
            if (acc.Username == Username && acc.Password == Password)
            {
                Favoriete_Film.Add("titel", movietitel); //Hier wordt de film in een Dict gezet
                Favoriete_Film.Add("Director", filmdirector);
                acc.savedInformationlist.Add(Favoriete_Film); //De list heeft <string, string> dicts

                string jsonttext = JsonConvert.SerializeObject(accounts, Formatting.Indented);
                File.WriteAllText(JsonFilePath, jsonttext);


                Console.WriteLine("Succesvol toegevoegd aan favoriete!");
            }
        }
    }
}
