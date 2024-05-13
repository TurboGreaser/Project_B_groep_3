namespace Project_B;

public static class AccountMenuQ
{
    // public static void Choose()
    // {
    //     Console.WriteLine("Je hebt 3 opties, antwoord met het getal.\n1 : Maak een account\n2 : Log In\n3 : doorgaan zonder account");
    //     string answer = Console.ReadLine();
    //     Console.WriteLine($"Weet je zeker dat je optie {answer} wilt?, Antwoord met 'ja' of 'nee'");
    //     string yn = Console.ReadLine().ToUpper();
    //     if (yn == "JA")
    //     {
    //         if (answer == "1")
    //         {
    //             AddAccount.MakeAccount();
    //         }
    //         if (answer == "2")
    //         {
    //             Console.WriteLine("Vul je email in.");
    //             string email = Console.ReadLine();
    //             Console.WriteLine("Vul je wachtwoord in.");
    //             string password = Console.ReadLine();
    //             LoginAccount.Login(email, password);
    //         }
    //         else
    //         {
    //             return;
    //         }
    //     }
    //     return;
    // }

    public static void Choose()
    {
        string[] MenuOptions = ["Maak een account", "Log In", "doorgaan zonder account"];
        int CurrentOption = 0;
        while (true)
        {
            Console.Clear();
            for (int i = 0; i < MenuOptions.Length; i++)
            {
                if (i == CurrentOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("--> ");
                }
                else
                {
                    Console.Write("    ");
                }
                Console.WriteLine(MenuOptions[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo KeyInfo = Console.ReadKey(true);
            switch (KeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    CurrentOption = (CurrentOption == 0)? MenuOptions.Length - 1 : CurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    CurrentOption = (CurrentOption == MenuOptions.Length - 1)?  0 : CurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    if (CurrentOption == 0)
                    {
                        AddAccount.MakeAccount();
                        return;
                    }
                    else if (CurrentOption == 1)
                    {
                        Inloggen();
                        break;
                    }
                    else 
                    {
                        return;
                    }
            }
        }
    }

    public static void Inloggen()
    {
        bool Valid = false;
        do
        {
            Console.WriteLine("Vul je email in.");
            try
            {
                string email = Console.ReadLine();
                Valid = true;
                Console.WriteLine("Vul je wachtwoord in.");
                string password = Console.ReadLine();
                return LoginAccount.Login(email, password);
            }
            catch (IOException) { }

            catch (Exception) { }
        }while (!Valid);
        
    }

    
}