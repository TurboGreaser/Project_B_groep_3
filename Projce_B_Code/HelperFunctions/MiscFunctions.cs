namespace Project_B;

public static class MiscFunctions
{
    public static string ReadLinePasswordHider()
    {
        string password = "";
        bool passwordVisible = false;
        ConsoleKeyInfo keyInfo;
        Console.WriteLine("Druk op [Tab] om je wachtwoord zichtbaar te maken");

        do
        {
            keyInfo = Console.ReadKey(intercept: true);

            // press backspace
            if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                // if password is longer than 0 rmoves last char from password
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            // press any key expet tab or space
            else if (!char.IsControl(keyInfo.KeyChar) && keyInfo.Key != ConsoleKey.Spacebar)
            {

                // Add character to password and display letter
                if (passwordVisible)
                {
                    password += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
                // Add character to password and display *
                else
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }

            }
            // press Tab
            else if (keyInfo.Key == ConsoleKey.Tab)
            {
                if (passwordVisible)
                {
                    // first clear the password
                    for (int i = 0; i < password.Length; i++)
                    {
                        Console.Write("\b \b");
                    }
                    // then write the *
                    for (int i = 0; i < password.Length; i++)
                    {
                        Console.Write("*");
                    }
                }
                else
                {
                    // first clear the stars
                    for (int i = 0; i < password.Length; i++)
                    {
                        Console.Write("\b \b");
                    }
                    // then print the password
                    Console.Write(password);

                }
                passwordVisible = !passwordVisible;

            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        // clear the * from console
        for (int i = 0; i < password.Length; i++)
        {
            Console.Write("\b \b");
        }
        return password;
    }
}
