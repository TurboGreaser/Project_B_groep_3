namespace Project_B;
public static class Reservation
{
    public static void MakeReservation(Film film, string date, int age, string email)
    {
        Zaal zaal = GetZaalFromFilm(film, date);
        Console.Clear();
        Console.WriteLine($"Je maakt een reservering voor: {film.Name} door: {film.Director}");
        Console.WriteLine($"Op {date} in zaal {zaal.ID}");
        Console.WriteLine($"klik Enter om veder te gaan naar het kiezen van je stoel");
        Console.ReadLine();


        double price = Json_writer.WriteReservationToJSON(film, zaal, date, age, email: email);
        if (price != -1)
        {
            ConfirmationMessage.ShowConfirmationMessage(film, date, email, price);
            Console.WriteLine("Druk op [Enter] om veder te gaan");
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
            Console.WriteLine("Klik enter om naar menu te gaan");
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

        Console.WriteLine("Film heeft geen Showings Default zaal wordt gebruikt");
        return zalen[0];
    }

    public static bool PrintPrice(double basePrice, double seatFee, double ageFee, int seatCount, int luxurySeatCount)
    {
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // :C formats the string to print as euro
            Console.Clear();
            Console.WriteLine("De totale prijs is");
            Console.WriteLine($"Film: {basePrice * seatCount:C} ({basePrice:C} * {seatCount})");
            Console.WriteLine($"Luxe Zitplaats: {seatFee * luxurySeatCount:C} ({basePrice * 0.1:C} * {luxurySeatCount})");
            Console.WriteLine($"onder 18 tarief: {ageFee * seatCount:C} ({basePrice * 0.2:C} * {(ageFee == 0 ? "0" : seatCount)})");
            Console.WriteLine($"------------------------------------------ +");
            Console.WriteLine($"Totaal: {basePrice * seatCount + seatFee + ageFee:C}");


            Console.WriteLine($"Betalen met:\n");
            int choice = ChooseOption();

            if (choice == 1)
            {
                Console.Clear();
                Console.WriteLine($"Ideal Betaling van {basePrice * seatCount + seatFee + ageFee:C} success!");
                Console.WriteLine($"Klik enter om veder te gaaan");
                Console.ReadLine();
                return true;

            }
            else if (choice == 2)
            {
                Console.Clear();
                Console.WriteLine($"Betaal: {basePrice * seatCount + seatFee + ageFee:C} aan de balie ");
                Console.WriteLine($"Klik enter om veder te gaaan");
                Console.ReadLine();
                return true;
            }
            else if (choice == 3)
            {
                Console.Clear();
                Console.WriteLine($"Reservering geanuleerd.");
                Console.WriteLine($"Klik enter Terug te gaan naar het menu");
                return false;
            }
        }
    }


    public static int ChooseOption()
    {
        Console.WriteLine("Gebruik pijltoetsen om te kiezen:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("-> Ideal");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("   Op locatie");
        Console.WriteLine("   Resevatie annuleren");

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
            case 0: return "Ideal";
            case 1: return "Op locatie";
            case 2: return "Resevatie annuleren";
            default: return "";
        }
    }


}


