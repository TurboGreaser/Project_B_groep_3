namespace Project_B;
using Newtonsoft.Json;

public static class AddAccount
{
    public static string jsonfilepath = "Accounts.json";
    public static string UserName;
    public static string Email;
    public static int Age;
    public static string Password;

    public static void MakeAccount()
    {
        Console.WriteLine("De terms en conditions zijn als volgt:\nDoor gebruik te maken van deze dienst gaat u akkoord met deze Algemene Voorwaarden.\n Indien u niet akkoord gaat met deze voorwaarden, dient u geen gebruik te maken van de dienst.");
        Console.WriteLine("Druk op een knop om verder te gaan");
        Console.ReadKey(true);
        Console.WriteLine("Acepteer je de terms en conditions? ");

        string [] JaOfNee = ["Ja", "Nee"];
        int CurrentOption = 0;
        while (true)
        {
            Console.Clear();
            for (int i = 0; i < 2; i++)
            {
                if (i == CurrentOption)
                {
                    Console.Write("--> ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.Write("    ");
                }
                Console.WriteLine(JaOfNee[i]);
                Console.ResetColor();
            }
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    CurrentOption = (CurrentOption == 0)? 1 : 0;
                    break;

                case ConsoleKey.DownArrow:
                    CurrentOption = (CurrentOption == 0)? 1 : 0;
                    break;
                
                case ConsoleKey.Enter:
                    if (CurrentOption == 0)
                    {
                        Gegevens();
                        return;
                    }
                    else
                    {
                        return;
                    }
            }
        }
    }


    public static void Gegevens()
//     {
//         Console.WriteLine("De terms en conditions zijn als volgt:\nDoor gebruik te maken van deze dienst gaat u akkoord met deze Algemene Voorwaarden. Indien u niet akkoord gaat met deze voorwaarden, dient u geen gebruik te maken van de dienst.\nAccepteer je de terms en conditions?\n(ja/nee)");
//         string acceptedTandC = Console.ReadLine().ToUpper();


//         if (acceptedTandC == "JA")
        {

            string text = File.ReadAllText(jsonfilepath);
            var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

            bool valid = false;
            do
            {
                Console.WriteLine("Enter je gewilde Gebruikersnaam:");
                string wantedusername = Console.ReadLine();
                //Kijk of de UserName all bestaat in de Json, als het nog niet bestaat in de Json dan mag de gebruiker deze UserName hebben anders niet.
                bool usernamexists = false;
                foreach (var user in useraccounts)
                {
                    if (wantedusername.ToUpper() == user.Username.ToUpper())
                    {
                        Console.WriteLine("De naam is al in gebruik, neem een andere naam");
                        usernamexists = true;
                        break;
                    }

                }

                if (usernamexists == false)
                {
                    valid = true;
                    UserName = wantedusername;
                }

            } while (valid == false);

            //Nu email
            do
            {
                bool emailexists = false;
                Console.WriteLine("Enter je Email: ");
                string wantedemail = Console.ReadLine();
                Console.WriteLine("Enter je Email opnieuw: ");
                string wantedemail2 = Console.ReadLine();
                //Kijk of de email al bestaat:


                if (wantedemail != wantedemail2)
                {
                    Console.WriteLine("De emails zijn niet gelijk.");
                    emailexists = true;
                }
                foreach (var per in useraccounts)
                {
                    if (wantedemail == per.Email)
                    {
                        Console.WriteLine("De email is al in gebruik, weet je zeker dat je de correcte email hebt ingevoerd?");
                        emailexists = true;
                    }

                }

                if (emailexists == false)
                {
                    valid = true;
                    Email = wantedemail;
                }
                else
                {
                    valid = false;
                }

            } while (valid == false);

            //nu age
            do
            {
                Console.WriteLine("Enter je leeftijd");
                int enteredage;

                try
                {
                    enteredage = Convert.ToInt32(Console.ReadLine());
                    valid = true;
                    Age = enteredage;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter een valide nummer");
                    valid = false;
                }
            } while (valid == false);

            //Nu wachtwoord
            do
            {
                Console.WriteLine("Enter je wachtwoord (moet meer dan 3 karakters hebben)");
                string firstentpassword = Console.ReadLine();
                Console.WriteLine("To confirm your Password Enter it again");
                string secondentpassword = Console.ReadLine();

                if (firstentpassword == secondentpassword && firstentpassword.Length > 3)
                {
                    valid = true;
                    Password = Stringcode.Base64Encode(firstentpassword);
                }
                else
                {
                    if (firstentpassword.Length <= 3)
                    {

                        Console.WriteLine("The password is not above 3 Characters");
                        valid = false;
                    }
                    else
                    {
                        Console.WriteLine("The first and second password entered were not the same!");
                        valid = false;
                    }
                }
            } while (valid == false);

            //Stuur deze Info naar de Json class waarin het naar json wordt gestuurd
            AddAccountToJson addnewacc = new(UserName, Email, Age, Password);
            addnewacc.AddToJson();

        }
//         else
//         {
//             Console.WriteLine("You did not accept the Terms and Conditions and will now be put back to the main menu");
//             //Gooi uit de class
//         }
//     }
}
