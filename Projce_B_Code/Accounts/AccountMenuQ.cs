// namespace Project_B;

// public static class AccountMenuQ
// {


//     public static Accounts Choose()
//     {
//         string[] MenuOptions = ["Maak een account", "Log In", "doorgaan zonder account"];
//         int CurrentOption = 0;
//         while (true)
//         {
//             Console.Clear();
//             for (int i = 0; i < MenuOptions.Length; i++)
//             {
//                 if (i == CurrentOption)
//                 {
//                     Console.ForegroundColor = ConsoleColor.DarkGreen;
//                     Console.Write("--> ");
//                 }
//                 else
//                 {
//                     Console.Write("    ");
//                 }
//                 Console.WriteLine(MenuOptions[i]);
//                 Console.ResetColor();
//             }

//             ConsoleKeyInfo KeyInfo = Console.ReadKey(true);
//             switch (KeyInfo.Key)
//             {
//                 case ConsoleKey.UpArrow:
//                     CurrentOption = (CurrentOption == 0) ? MenuOptions.Length - 1 : CurrentOption - 1;
//                     break;
//                 case ConsoleKey.DownArrow:
//                     CurrentOption = (CurrentOption == MenuOptions.Length - 1) ? 0 : CurrentOption + 1;
//                     break;
//                 case ConsoleKey.Enter:
//                     if (CurrentOption == 0)
//                     {

//                         var emailPasssTuple = AddAccount.MakeAccount();
//                         return LoginAccount.Login(emailPasssTuple.Item1, emailPasssTuple.Item2);
//                     }
//                     else if (CurrentOption == 1)
//                     {
//                         return Inloggen();
//                     }
//                     else
//                     {
//                         return null;
//                     }
//             }
//         }
//     }

//     public static Accounts Inloggen()
//     {
//         bool Valid = false;
//         do
//         {
//             Console.WriteLine("Vul je email in.");
//             try
//             {
//                 string email = Console.ReadLine();
//                 Valid = true;
//                 Console.WriteLine("Vul je wachtwoord in.");
//                 string password = Console.ReadLine();
//                 return LoginAccount.Login(email, password);
//             }
//             catch (IOException) { }

//             catch (Exception) { }
//         } while (!Valid);
//         return null;

//     }


// }    


//???????????????????????????????????????????????????????????????????????????????????????????????????///
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


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Project_B
{
    public static class AccountMenuQ
    {
        private static string jsonFilePath = "Accounts.json";

        public static Accounts Choose()
        {
            string[] MenuOptions = new string[] { "Maak een account", "Log In", "Doorgaan zonder account" };
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
                        CurrentOption = (CurrentOption == 0) ? MenuOptions.Length - 1 : CurrentOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        CurrentOption = (CurrentOption == MenuOptions.Length - 1) ? 0 : CurrentOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        if (CurrentOption == 0)
                        {
                            var emailPasssTuple = AddAccount.MakeAccount();
                            return LoginAccount.Login(emailPasssTuple.Item1, emailPasssTuple.Item2);
                        }
                        else if (CurrentOption == 1)
                        {
                            return Inloggen();
                        }
                        else
                        {
                            return null;
                        }
                }
            }
        }

        public static Accounts Inloggen()
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
            } while (!Valid);
            return null;
        }
    }
}
