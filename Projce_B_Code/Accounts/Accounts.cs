namespace Project_B;

public class SavedInformation
{
    public List<string> FavMovies { get; set; }
}

public class Accounts
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Password { get; set; }
    public string SecondPassword { get; set; }
    public List<Dictionary<string, string>> savedInformationlist { get; set; }
    public bool IsLoggedIn { get; set; } = false;
}