using System.Net.Http.Headers;

namespace Project_B;

public static class SeatSelection
{
    private static int SelectSeatCode()
    {
        bool selecting_seat = true;
        int size = 7;
        List<int> unavailableSeats = new List<int> { 1, 5, 12, 18 };

        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;
        do
        {
            Console.Clear();

            // Display seats
            int index = 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.ForegroundColor = unavailableSeats.Contains(index) ? ConsoleColor.Red : (selectedIndex == index - 1 ? ConsoleColor.Cyan : ConsoleColor.Green);
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
                        Console.WriteLine($"Are you sure?");
                        Console.WriteLine($"Yes  No");
                        bool Choosing = true;
                        string yes_no = "yes";

                        while (Choosing)
                        {
                            Console.Clear();
                            Console.WriteLine($"You selected seat {selectedSeat}");
                            Console.WriteLine("Are you sure?");
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
                                            // Choosing = false;
                                            // selecting_seat = false;

                                            Console.WriteLine($"You have selected seat {selectedSeat}");
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
            Console.Write("Yes");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" No");
        }
        else if (yes_no == "no")
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("Yes ");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("No");
        }
        else
        {
            Console.Write("Yes No");

        }
    }
    public static int SelectSeat()
    {
        int seat = -1;
        while (seat < 0)
        {
            seat = SelectSeatCode();
        }
        return seat;
    }

}

