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
        Console.WriteLine("De terms en conditions zijn als volgt:\nDoor gebruik te maken van deze dienst gaat u akkoord met deze Algemene Voorwaarden. Indien u niet akkoord gaat met deze voorwaarden, dient u geen gebruik te maken van de dienst.\nAccepteer je de terms en conditions?\n(ja/nee)");
        string acceptedTandC = Console.ReadLine().ToUpper();


        if (acceptedTandC == "JA")
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
                Console.WriteLine("Enter je Email (Moet een '@' en een '.com' hebben!): ");
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
                if(wantedemail.Contains("@") == false || wantedemail.Contains(".com") == false) //Check if de email een .com of @ heeft.
                {
                    Console.WriteLine("Je email miste een '@' of een '.com'");
                    emailexists = true;
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
                    
                    if(enteredage < 0 || enteredage > 111)  //Check of leeftijd tussen 0 en 111 is.
                    {
                        valid = false;
                    }
                    else
                    {
                        valid = true;
                        Age = enteredage;
                    }
                    
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
                Console.WriteLine("Enter je wachtwoord (moet meer dan 5 karakters hebben) (En 1 special teken)");
                string firstentpassword = Console.ReadLine();
                Console.WriteLine("Voor confermatie vul je wachtwoord opnieuw in");
                string secondentpassword = Console.ReadLine();
                bool hasspecialletter = false;

                if (firstentpassword == secondentpassword && firstentpassword.Length > 5)
                {
                    foreach (char c in firstentpassword)
                    {
                        if (!char.IsLetterOrDigit(c)) //Check of het een special teken heeft
                        {
                            hasspecialletter = true;
                        }
                    }

                    if(hasspecialletter == true)
                    {
                        valid = true;
                        Password = Stringcode.Base64Encode(firstentpassword);
                    }
                    else
                    {
                        valid = false;
                        Console.WriteLine("\nJe wachtwoord had geen speciaal teken!\n");
                    }
                }


                else
                {
                    if (firstentpassword.Length <= 5)
                    {

                        Console.WriteLine("\nhet wachtwoord is onder 5 karakters\n");
                        valid = false;
                    }
                    else
                    {
                        Console.WriteLine("\nhet eerste en tweede wachtwoord zijn niet gelijk!\n");
                        valid = false;
                    }
                }
            } while (valid == false);

            //Stuur deze Info naar de Json class waarin het naar json wordt gestuurd
            AddAccountToJson addnewacc = new(UserName, Email, Age, Password);
            addnewacc.AddToJson();

        }
        else
        {
            Console.WriteLine("Je hebt de Terms en conditions niet geaccepteerd!");
            //Gooi uit de class
        }
    }
}
