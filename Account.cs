using Newtonsoft.Json;
public class Account
{
    public string Naam;
    public string Email;
    public int Age;
    public string Password;

    public string jsonfilepath = "Accounts.json";

    /*
    public account(string naam, string email, int age, string password)
    {
        naam = naam;
        email = email;
        age = age;
        password = password;
    }

    gebruik dit hierboven als er in de main file een email enzo wordt meegegeven.
    */

    public void MakeAccount()
    {
        Console.WriteLine("You can create an account here, for this you will need to enter your wanted username, email, age and a wanted password\nDo you accept the Terms and Conditions?\n(yes/no)");
        string acceptedTandC = Console.ReadLine().ToUpper();


        if (acceptedTandC ==  "YES")
        {
            Console.WriteLine("Enter your wanted username:");
            string wantedusername = Console.ReadLine();
            //Kijk of de UserName all bestaat in de Json, als het nog niet bestaat in de Json dan mag de gebruiker deze UserName hebben anders niet.



        }


        else
        {
            Console.WriteLine("You did not accept the Terms and Conditions and will now be put back to the main menu");
            //Gooi uit de class
        }
    }
}