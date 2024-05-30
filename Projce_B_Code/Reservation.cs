namespace Project_B;
public static class Reservation
{
    public static void MakeReservation(Film film, string date, int age, string email)
    {
        Zaal zaal = GetZaalFromFilm(film, date);
        Console.Clear();
        Console.WriteLine($"U maakt een reservering voor: {film.Name} door: {film.Director}");
        Console.WriteLine($"Op {date} in Zaal {zaal.ID}");
        Console.WriteLine($"Klik Enter om je zitplaats te kiezen!");
        Console.ReadLine();


        double price = Json_writer.WriteReservationToJSON(film, zaal, date, age, email: email);
        if (price != -1)
        {
            ConfirmationMessage.ShowConfirmationMessage(film, date, email, price);
            Console.WriteLine("Druk op [Enter] om verder te gaan");
            Console.ReadLine();
            if (email == "NoEmail")
            {
                Console.WriteLine("Wilt u nog een account aanmaken, zodat u de volgende keer NOG sneller kunt reserveren?");
                if (SeatSelection.ChooseOption())
                {
                    AddAccount.MakeAccount();
                }
            }
            Console.WriteLine("Dank u voor het reserveren, tot de volgende keer!");
            Console.WriteLine("Klik Enter om naar het menu te gaan");
        }
        Console.ReadLine();
    }







    private static Zaal GetZaalFromFilm(Film film, string date)
    {
        List<Zaal> zalen = JsonReader.ReadZalen();



        foreach (Zaal zaal in zalen)
        {
            foreach (var showing in film.Showings)
            {
                if (showing.Datum == date && showing.Zaal == zaal.ID)
                {
                    return zaal;
                }
            }
        }

        Console.WriteLine("Film heeft geen showings, er wordt een standaard zaal gebruikt");
        return zalen[0];
    }

    public static bool PrintPrice(double basePrice, double seatFee, double ageFee, int seatCount, int luxurySeatCount)
    {
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            Console.WriteLine("De totale prijs is: ");
            
            double totalPrice = 0;

       
            if (basePrice > 0 && seatCount > 0)
            {
                double baseTotal = basePrice * seatCount;
                Console.WriteLine($"Film prijs: {baseTotal:C} ");
                totalPrice += baseTotal;
            }

    
            if (seatFee > 0 && luxurySeatCount > 0)
            {
                double luxuryTotal = seatFee * luxurySeatCount;
                Console.WriteLine($"Luxe zitplaats: {luxuryTotal:C} ");
                totalPrice += luxuryTotal;
            }


            if (ageFee > 0 && seatCount > 0)
            {
                double ageTotal = ageFee * seatCount;
                Console.WriteLine($"Onder 18 tarief: {ageTotal:C} ");
                totalPrice += ageTotal;
            }

            Console.WriteLine($"------------------------------------------ +");
            Console.WriteLine($"Totale prijs: {totalPrice:C}");

            Console.WriteLine($"Betalen met:\n");
            int choice = ChooseOption();

            if (choice == 1)
            {
                Console.Clear();
                Console.WriteLine($"De betaling met Ideal is succesvol!. U heeft dit bedrag betaald: {totalPrice:C}. Tot gauw!");
                Console.WriteLine($"Klik Enter om verder te gaan");
                Console.ReadLine();
                return true;
            }
            else if (choice == 2)
            {
                Console.Clear();
                Console.WriteLine($"U heeft gekozen om te betalen op locatie. We vragen aan u om dit bedrag mee te nemen: {totalPrice:C}. Tot gauw! ");
                Console.WriteLine($"Klik Enter om verder te gaan");
                Console.ReadLine();
                return true;
            }
            else if (choice == 3)
            {
                Console.Clear();
                Console.WriteLine($"U heeft uw reservering geannuleerd.");
                Console.WriteLine($"Klik Enter om terug te gaan naar het menu");
                return false;
            }
        }
    }



    public static int ChooseOption()
    {
        Console.WriteLine("Gebruik de pijltoetsen om te kiezen:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("-> Met Ideal betalen");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("   Op locatie betalen");
        Console.WriteLine("   Reservering annuleren");

        int selectedIndex = 0;
        int optionCount = 3;

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex - 1 + optionCount) % optionCount;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % optionCount;
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.ForegroundColor = ConsoleColor.White;
                return selectedIndex + 1; // Adding 1 to make options start from 1 instead of 0
            }

            Console.SetCursorPosition(0, Console.CursorTop - optionCount); // Move cursor up
            Console.CursorVisible = false;

            for (int i = 0; i < optionCount; i++)
            {
                Console.ForegroundColor = selectedIndex == i ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine(selectedIndex == i ? $"-> {GetOptionName(i)}" : $"   {GetOptionName(i)}");
            }
        }
    }

    private static string GetOptionName(int index)
    {
        switch (index)
        {
            case 0: return "Met Ideal betalen";
            case 1: return "Op locatie betalen";
            case 2: return "Reservering annuleren";
            default: return "";
        }
    }


}


