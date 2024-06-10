namespace Project_B;

public static class FormattedOutput
{
    public static void Display_Title()
    {
        Console.WriteLine(@"
  ____  _____ _____ ____   ___     ____ ___ _   _ _____ __  __    _    
 |  _ \| ____|_   _|  _ \ / _ \   / ___|_ _| \ | | ____|  \/  |  / \   
 | |_) |  _|   | | | |_) | | | | | |    | ||  \| |  _| | |\/| | / _ \  
 |  _ <| |___  | | |  _ <| |_| | | |___ | || |\  | |___| |  | |/ ___ \ 
 |_| \_\_____| |_| |_| \_\\___/   \____|___|_| \_|_____|_|  |_/_/   \_\
                                                                       
");
    }

    public static void Display_Todays_Films()
    {
        DateTime DateOfToday = DateTime.Now.Date;
        string Films2Day = "";

        foreach (Film f in MainMenu.films)
        {
            foreach (Showing s in f.Showings)
            {
                DateTime showingDate = DateTime.Parse(s.Datum).Date;
                if (showingDate == DateOfToday)
                {

                    Films2Day += $"+--------------------------------------------+\n";
                    Films2Day += $"| Title: {f.Name,-35} |\n";
                    Films2Day += $"| Time: {DateTime.Parse(s.Datum).ToShortTimeString(),-36} |\n";
                    Films2Day += $"+--------------------------------------------+\n\n";
                    break;

                }
            }
        }

        if (!string.IsNullOrEmpty(Films2Day))
        {
            Console.WriteLine("Films Die vandaag draaien:\n");
            Console.WriteLine(Films2Day);
        }
        else
        {
            Console.WriteLine("Er draaien vandaag geen films.");
        }
    }

    public static void Display_FilmList()
    {
        Console.WriteLine(@"
  _____ _ _             _     _  _     _   
 |  ___(_) |_ __ ___   | |   (_)(_)___| |_ 
 | |_  | | | '_ ` _ \  | |   | || / __| __|
 |  _| | | | | | | | | | |___| || \__ \ |_ 
 |_|   |_|_|_| |_| |_| |_____|_|/ |___/\__|
                              |__/         
");
    }

    public static void DisplayRegister()
    {
        Console.WriteLine(@"
  ____  _____ ____ ___ ____ _____ ____  _____ ____  _____ _   _ 
 |  _ \| ____/ ___|_ _/ ___|_   _|  _ \| ____|  _ \| ____| \ | |
 | |_) |  _|| |  _ | |\___ \ | | | |_) |  _| | |_) |  _| |  \| |
 |  _ <| |__| |_| || | ___) || | |  _ <| |___|  _ <| |___| |\  |
 |_| \_\_____\____|___|____/ |_| |_| \_\_____|_| \_\_____|_| \_|
                                                                
");
    }

    public static void Display_RestaurantMenu()
    {
        Console.WriteLine(@"
  ____  _____ ____  ____      _   _   _ ____      _    _   _ _____   __  __ _____ _   _ _   _ 
 |  _ \| ____/ ___||  _ \    / \ | | | |  _ \    / \  | \ | |_   _| |  \/  | ____| \ | | | | |
 | |_) |  _| \___ \| |_) |  / _ \| | | | |_) |  / _ \ |  \| | | |   | |\/| |  _| |  \| | | | |
 |  _ <| |___ ___) |  _ <  / ___ \ |_| |  _ <  / ___ \| |\  | | |   | |  | | |___| |\  | |_| |
 |_| \_\_____|____/|_| \_\/_/   \_\___/|_| \_\/_/   \_\_| \_| |_|   |_|  |_|_____|_| \_|\___/ 
                                                                                              
");
    }

}