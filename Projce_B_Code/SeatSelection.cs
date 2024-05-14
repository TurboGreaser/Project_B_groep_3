using System.Net.Http.Headers;

namespace Project_B;

public static class SeatSelection
{
    private static int SelectSeatCode(int size, List<int> unavailableSeats)
    {
        bool selecting_seat = true;
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            Console.Clear();

            // Display seats
            int index = 1;
            List<int> expensiveSeats = SeatSaleRoom.GetMiddleSeats(size);

            Console.WriteLine("De gele Stoelen zijn 10% duurder\n");
            Console.WriteLine("Kies een stoel met de pijtjes toetsen, Rood = Bezet");
<<<<<<< Updated upstream
            Console.WriteLine("De gele Stoelen zijn 20% duurder");
=======
            Console.WriteLine("Druk op (Esc) om terug te gaan naar het menu");
            Console.WriteLine("Druk op (Enter) om een stoel te kiezen");
            Console.WriteLine("Druk op (C) om verder te gaan naar betalen ");

>>>>>>> Stashed changes
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Console.ForegroundColor = unavailableSeats.Contains(index) ? ConsoleColor.Red : (selectedIndex == index - 1 ? ConsoleColor.Cyan : ConsoleColor.Green);
                    if (unavailableSeats.Contains(index))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (selectedIndex == index - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (expensiveSeats.Contains(index))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.BackgroundColor = selectedIndex == index - 1 ? ConsoleColor.Gray : ConsoleColor.Black;
                    Console.Write($"{index}".PadLeft(4));
                    index++;
                }
                Console.WriteLine();
            }
            Console.ResetColor();

            // Get user input
            keyInfo = Console.ReadKey();

            // Process user input
            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape:
                    {
                        Console.Clear();
                        return -10;
                    }
                case ConsoleKey.C:
                    {
                        Console.Clear();
                        return -100;
                    }
                case ConsoleKey.UpArrow:
                    selectedIndex = Math.Max(0, selectedIndex - size);
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = Math.Min(size * size - 1, selectedIndex + size);
                    break;

                case ConsoleKey.LeftArrow:
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    break;

                case ConsoleKey.RightArrow:
                    selectedIndex = Math.Min(size * size - 1, selectedIndex + 1);
                    break;

                case ConsoleKey.Enter:
                    int selectedSeat = selectedIndex + 1;
                    Console.Clear();

                    if (unavailableSeats.Contains(selectedSeat))
                    {
                        Console.WriteLine($"Seat {selectedSeat} is already taken");
                        Console.ReadLine(); // Pause for user to see the result
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Je hebt gekozen voor de zitplaats {selectedSeat}");
                        Console.WriteLine("Weet je het zeker?");
                        if (ChooseOption())
                        {
                            // yes
                            return selectedSeat;
                        }
                        else
                        {
                            // no
                            return -1;
                        }
                    }
                    // break for Enter case
                    break;
            }
            continue;
        } while (selecting_seat);
        return selectedIndex + 1;



    }

    public static List<int> SelectSeat(int theathre_size, List<int> unavailableSeats)
    {
        List<int> seats = new() { };
        int seat = -1;
        while (seat < 0)
        {
            // get seat returns -1 if user chooses "nee" bij weet je het zeker
            seat = SelectSeatCode(theathre_size, unavailableSeats);
            unavailableSeats.Add(seat);

            if (seat == -10)
            {
                return null;
            }
            if (seat == -100)
            {
                if (seats.Count != 0)
                { break; }
                else
                {
                    Console.WriteLine("Je moet een zitplaats kiezen om veder te gaan");
                    Console.WriteLine("\nDruk Enter om verder te gaan");
                    Console.ReadLine();

                }
            }
            else if (seat != -1)
            {
                seats.Add(seat);
                Console.Clear();

                Console.WriteLine($"Je hebt voor stoel {seat} gekozen");
                Console.WriteLine($"Wil nog een stoel kiezen?");
                // choose ja to choose another seat
                if (ChooseOption())
                {
                    // set seat to -1 to continue loop
                    seat = -1;
                }
                else
                {
                    break;
                }
            }
        }
<<<<<<< Updated upstream
        Console.WriteLine($"You have selected seat {seat}");
        Console.WriteLine($"You have selected seat {seat}");
=======
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        string massage = seats.Count() == 1 ? "stoel" : "de stoelen";
        Console.WriteLine($"Je hebt {massage} {ConvertIntListToString(seats)} gekozen");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\nDruk Enter om verder te gaan");
>>>>>>> Stashed changes
        Console.ReadLine();
        return seats;
    }


    public static bool ChooseOption()
    {
        Console.WriteLine("Gebruik pijltoetsen om te kiezen:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("-> Ja");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("   Nee");

        int selectedIndex = 0;
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedIndex = Math.Max(0, selectedIndex - 1);
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = Math.Min(1, selectedIndex + 1);
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.ForegroundColor = ConsoleColor.White;
                return selectedIndex == 0;
            }

            Console.SetCursorPosition(0, Console.CursorTop - 2); // Verplaats cursor omhoog
            Console.CursorVisible = false;

            Console.ForegroundColor = selectedIndex == 0 ? ConsoleColor.Green : ConsoleColor.White;
            Console.WriteLine(selectedIndex == 0 ? "-> Ja" : "   Ja");
            Console.ForegroundColor = selectedIndex == 1 ? ConsoleColor.Green : ConsoleColor.White;
            Console.WriteLine(selectedIndex == 1 ? "-> Nee" : "   Nee");
        }
    }

    private static string ConvertIntListToString(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            return "";
        }

        string result = numbers[0].ToString();
        for (int i = 1; i < numbers.Count; i++)
        {
            result += ", " + numbers[i];
        }
        return result;
    }
}





