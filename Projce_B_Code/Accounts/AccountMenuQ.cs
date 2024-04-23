namespace Project_B;

public static class AccountMenuQ
{
    public static Accounts Choose()
    {
        Console.WriteLine("Je hebt 3 opties, antwoord met het getal.\n1 : Maak een account\n2 : Log In\n3 : doorgaan zonder account");
        string answer = Console.ReadLine();
        Console.WriteLine($"Weet je zeker dat je optie {answer} wilt?, Antwoord met 'ja' of 'nee'");
        string yn = Console.ReadLine().ToUpper();
        if (yn == "JA")
        {
            if (answer == "1")
            {
                AddAccount.MakeAccount();
            }
            if (answer == "2")
            {
                Console.WriteLine("Vul je email in.");
                string email = Console.ReadLine();
                Console.WriteLine("Vul je wachtwoord in.");
                string password = Console.ReadLine();
                return LoginAccount.Login(email, password);
            }
            else
            {
                return null;
            }
        }
        return null;
    }
}