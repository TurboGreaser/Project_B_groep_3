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

            Console.WriteLine("Escape om terug te gaan");
            Console.WriteLine("Kies een stoel met de pijtjes toetsen, Rood = Bezet");
            Console.WriteLine("De gele Stoelen zijn 10% duurder");
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
                        Console.WriteLine($"Stoel {selectedSeat} is al bezet");
                        Console.ReadLine(); // Pause for user to see the result
                    }
                    else
                    {
                        Console.WriteLine($"Are you sure?");
                        Console.WriteLine($"Yes  No");
                        bool Choosing = true;
                        string yes_no = "yes";

                        while (Choosing)
                        {
                            Console.Clear();
                            Console.WriteLine($"Je hebt gekozen voor de zitplaats {selectedSeat}");
                            Console.WriteLine("Weet je het zeker?");
                            Draw(yes_no);
                            Console.WriteLine();
                            Console.ResetColor();
                            keyInfo = Console.ReadKey();

                            switch (keyInfo.Key)
                            {
                                case ConsoleKey.LeftArrow:
                                    {
                                        yes_no = "yes";
                                        break;
                                    }
                                case ConsoleKey.RightArrow:
                                    {

                                        yes_no = "no";
                                        break;
                                    }
                                case ConsoleKey.Enter:
                                    {
                                        if (yes_no == "no")
                                        {
                                            return -1;

                                        }
                                        else
                                        {
                                            return selectedSeat;
                                        }
                                        // break;
                                    }
                            }

                        }
                    }
                    break;
            }
            continue;
        } while (selecting_seat);
        return selectedIndex + 1;



    }
    private static void Draw(string yes_no)
    {
        if (yes_no == "yes")
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("Ja");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" Nee");
        }
        else if (yes_no == "no")
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("Ja ");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("Nee");
        }
        else
        {
            Console.Write("Yes No");

        }
    }
    public static int SelectSeat(int theathre_size, List<int> unavailableSeats)
    {
        int seat = -1;
        while (seat < 0)
        {
            seat = SelectSeatCode(theathre_size, unavailableSeats);
            if (seat == -10)
            {
                return -1;
            }
        }
        Console.WriteLine($"Je hebt voor stoel {seat} gekozen");
        Console.ReadLine();
        return seat;
    }

}

