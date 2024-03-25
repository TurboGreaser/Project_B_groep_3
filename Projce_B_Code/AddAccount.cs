using Newtonsoft.Json;

public class AddAccount
{
    public string jsonfilepath = "Accounts.json";
    public string UserName;
    public string Email;
    public int Age;
    public string Password;

    public void MakeAccount()
    {
        Console.WriteLine("You can create an account here, for this you will need to enter your wanted username, email, age and a wanted password\nDo you accept the Terms and Conditions?\n(yes/no)");
        string acceptedTandC = Console.ReadLine().ToUpper();


        if (acceptedTandC == "YES")
        {

            string text = File.ReadAllText(jsonfilepath);
            var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

            bool valid = false;
            do
            {
                Console.WriteLine("Enter your wanted username:");
                string wantedusername = Console.ReadLine();
                //Kijk of de UserName all bestaat in de Json, als het nog niet bestaat in de Json dan mag de gebruiker deze UserName hebben anders niet.
                bool usernamexists = false;
                foreach (var user in useraccounts)
                {
                    if (wantedusername.ToUpper() == user.Username.ToUpper())
                    {
                        Console.WriteLine("The name was Already taken. Try another username");
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
                Console.WriteLine("Enter your Email: ");
                string wantedemail = Console.ReadLine();
                //Kijk of de email al bestaat:

                bool emailexists = false;
                foreach (var per in useraccounts)
                {
                    if (wantedemail == per.Email)
                    {
                        Console.WriteLine("This Email Is already in use Are you sure you entered the right email?");
                        emailexists = true;
                        break;
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
                Console.WriteLine("Enter your age");
                int enteredage;

                try
                {
                    enteredage = Convert.ToInt32(Console.ReadLine());
                    valid = true;
                    Age = enteredage;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid Number");
                    valid = false;
                }
            } while (valid == false);

            //Nu wachtwoord
            do
            {
                Console.WriteLine("Enter your Password (It must be above 3 Characters)");
                string firstentpassword = Console.ReadLine();
                Console.WriteLine("To confirm your Password Enter it again");
                string secondentpassword = Console.ReadLine();

                if (firstentpassword == secondentpassword && firstentpassword.Length > 3)
                {
                    valid = true;
                    Password = firstentpassword;
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
        else
        {
            Console.WriteLine("You did not accept the Terms and Conditions and will now be put back to the main menu");
            //Gooi uit de class
        }
    }
}
