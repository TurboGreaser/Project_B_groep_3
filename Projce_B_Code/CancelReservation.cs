using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
namespace Project_B;

public static class CancelReservation
{
    public static string jsonfilepath = "Reservations.json";
    public static string jsonfilepathfilms = "Films.json";

    public static void InfoFromUser(string emailToRemove)
    {
        string enteredChoice = "";

        do
        {
            Console.WriteLine("Weet u zeker dat u uw reservering wilt annuleren? (ja/nee)\n");
            enteredChoice = Console.ReadLine().ToLower();

            if (enteredChoice == "ja")
            {
                string text = File.ReadAllText(jsonfilepath);
                var reservations = JsonConvert.DeserializeObject<List<Reservations>>(text);

                DisplayReservationsForEmail(reservations, emailToRemove);
            }
            else if (enteredChoice != "nee")
            {
                Console.WriteLine("Typ 'ja' of 'nee'\n");
            }
            else
            {
                Console.WriteLine("Reservering is niet geannuleerd\n");
            }
        }
        while (enteredChoice != "ja" && enteredChoice != "nee");
    }

    public static void DisplayReservationsForEmail(List<Reservations> reservations, string email)
    {
        List<Reservations> matchingReservations = new List<Reservations>();

        foreach (var reservation in reservations)
        {
            if (reservation.Emails.Contains(email))
            {
                matchingReservations.Add(reservation);
            }
        }

        if (matchingReservations.Count == 0)
        {
            Console.WriteLine("Geen reserveringen gevonden voor het opgegeven e-mailadres.\n");
            return;
        }

        Console.WriteLine("Reserveringen gevonden voor het opgegeven e-mailadres:\n");

        int indexOfCurrentOption = 0;

        while (true)
        {
            Console.Clear();
            for (int i = 0; i < matchingReservations.Count; i++)
            {
                if (i == indexOfCurrentOption)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("--> ");
                }
                else
                {
                    Console.Write("    ");
                }
                Console.WriteLine($"{i + 1}. Reservering ID: {matchingReservations[i].ID}, Zaal: {matchingReservations[i].ZaalID}");
                Console.ResetColor();
            }

            Console.WriteLine("\nGebruik de pijltjestoetsen om een reservering te selecteren en druk op Enter om te annuleren, of typ '0' om niets te annuleren.");

            ConsoleKeyInfo keyInput = Console.ReadKey(true);
            switch (keyInput.Key)
            {
                case ConsoleKey.UpArrow:
                    indexOfCurrentOption = (indexOfCurrentOption == 0) ? matchingReservations.Count - 1 : indexOfCurrentOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    indexOfCurrentOption = (indexOfCurrentOption == matchingReservations.Count - 1) ? 0 : indexOfCurrentOption + 1;
                    break;
                case ConsoleKey.Enter:
                    Reservations reservationToModify = matchingReservations[indexOfCurrentOption];
                    HandleSeatSelection(reservationToModify, email);
                    return;
                case ConsoleKey.D0:
                    Console.WriteLine("Annulering geannuleerd.");
                    return;
            }
        }
    }

    public static void HandleSeatSelection(Reservations reservation, string email)
    {
        List<int> userSeats = GetSeatsForEmail(reservation, email);

        if (userSeats.Count == 1)
        {
            RemoveSeatFromReservation(reservation, email, userSeats[0]);
        }
        else
        {
            int indexOfCurrentOption = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Kies een stoel om te annuleren voor reservering {reservation.ID}:");

                for (int i = 0; i < userSeats.Count; i++)
                {
                    if (i == indexOfCurrentOption)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("--> ");
                    }
                    else
                    {
                        Console.Write("    ");
                    }
                    Console.WriteLine($"{i + 1}. Stoel: {userSeats[i]}");
                    Console.ResetColor();
                }

                Console.WriteLine("\nGebruik de pijltjestoetsen om een stoel te selecteren en druk op Enter om te annuleren, of typ '0' om niets te annuleren.");

                ConsoleKeyInfo keyInput = Console.ReadKey(true);
                switch (keyInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        indexOfCurrentOption = (indexOfCurrentOption == 0) ? userSeats.Count - 1 : indexOfCurrentOption - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        indexOfCurrentOption = (indexOfCurrentOption == userSeats.Count - 1) ? 0 : indexOfCurrentOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        RemoveSeatFromReservation(reservation, email, userSeats[indexOfCurrentOption]);
                        return;
                    case ConsoleKey.D0:
                        Console.WriteLine("Annulering geannuleerd.");
                        return;
                }
            }
        }
    }

    public static void RemoveSeatFromReservation(Reservations reservation, string email, int seatToRemove)
    {
        string text = File.ReadAllText(jsonfilepath);
        var reservations = JsonConvert.DeserializeObject<List<Reservations>>(text);

        int reservationIndex = reservations.FindIndex(r => r.ID == reservation.ID);

        if (reservationIndex != -1)
        {
            // Remove the specific seat
            reservations[reservationIndex].Seats.Remove(seatToRemove);
            reservations[reservationIndex].Emails.Remove(email);

            // Remove the email only if the email had no more seats reserved
            int emailIndex = reservation.Emails.IndexOf(email);
            reservation.Emails.RemoveAt(emailIndex);

            if (reservations[reservationIndex].Emails.Count == 0)
            {
                reservations.RemoveAt(reservationIndex);
                Console.WriteLine($"Reservering {reservation.ID} volledig geannuleerd omdat er geen e-mails meer zijn.");
            }

            string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText(jsonfilepath, updatedJson);

            Console.WriteLine($"Stoel {seatToRemove} voor reservering {reservation.ID} is succesvol geannuleerd!");
        }
        else
        {
            Console.WriteLine($"Reservering {reservation.ID} kon niet worden gevonden om te annuleren.");
        }
    }

    private static List<int> GetSeatsForEmail(Reservations reservation, string email)
    {
        List<int> seatsForEmail = new List<int>();

        for (int i = 0; i < reservation.Emails.Count; i++)
        {
            if (reservation.Emails[i] == email)
            {
                seatsForEmail.Add(reservation.Seats[i]);
            }
        }

        return seatsForEmail;
    }
    public static double GetMoviePrice(string movieName)
    {
        string text = File.ReadAllText(jsonfilepathfilms);
        var films = JsonConvert.DeserializeObject<List<Film>>(text);

        Film movie = films.Find(film => film.Name == movieName);

        if (movie != null)
        {
            return movie.Price;
        }
        else
        {
            Console.WriteLine("Film niet gevonden. Vergeet niet de hoofdletters!");
            return 0;
        }
    }
}