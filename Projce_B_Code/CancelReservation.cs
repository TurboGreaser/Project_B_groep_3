using Newtonsoft.Json;
namespace Project_B;
//using System.Collections.Generic;
//using System.IO;

public static class CancelReservation
{
    public static string jsonfilepath = "Reservations.json";
    public static string jsonfilepathfilms = "Films.json";

    public static void InfoFromUser(string emailToRemove)
    {
        string enteredChoice = "";

        do
        {
            Console.WriteLine("Weet u zeker dat u uw reservering wilt annuleren? (ja/nee)");
            enteredChoice = Console.ReadLine().ToLower();

            if (enteredChoice == "ja")
            {
                string text = File.ReadAllText(jsonfilepath);
                var reservations = JsonConvert.DeserializeObject<List<Reservations>>(text);

                DisplayReservationsForEmail(reservations, emailToRemove);
            }
            else if (enteredChoice != "nee")
            {
                Console.WriteLine("Typ 'ja' of 'nee'");
            }
            else
            {
                Console.WriteLine("Reservering is geannuleerd");
            }
        }
        while (enteredChoice != "ja" && enteredChoice != "nee");

    }

    public static void DisplayReservationsForEmail(List<Reservations> reservations, string email)
    {
        List<Reservations> matchingReservations = new List<Reservations>();

        
        foreach (var reservation in reservations)// alle reserveringen
        {
            if (reservation.Emails.Contains(email)) // emil adres toevoegen aan list
            {
                matchingReservations.Add(reservation);
            }
        }

        if (matchingReservations.Count == 0)
        {
            Console.WriteLine("Geen reserveringen gevonden voor het opgegeven e-mailadres.");
            return;
        }

        Console.WriteLine("Reserveringen gevonden voor het opgegeven e-mailadres:");
        for (int i = 0; i < matchingReservations.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Reservatie ID: {matchingReservations[i].ID}, Zaal: {matchingReservations[i].ZaalID}");
        }

        Console.Write("Geef het nummer van de reservering die u wilt annuleren, of typ '0' om te annuleren: ");

        
        int selectedOption;
        while (!int.TryParse(Console.ReadLine(), out selectedOption) || selectedOption < 0 || selectedOption > matchingReservations.Count) // verandert naar int en geeft daarna de int door aan selectedoption
        {
            Console.WriteLine("Ongeldige invoer. Probeer opnieuw.");
            Console.Write("Geef het nummer van de reservering die u wilt annuleren, of typ '0' om te annuleren: ");
        }

        if (selectedOption == 0)
        {
            Console.WriteLine("Reservering geannuleerd.");
            return;
        }

        Reservations reservationToRemove = matchingReservations[selectedOption - 1];
        Console.WriteLine($"U heeft gekozen om reservering {reservationToRemove.ID} te annuleren.");
        RemoveReservation(reservationToRemove);
    }


    public static void RemoveReservation(Reservations reservation)
    {
        string text = File.ReadAllText(jsonfilepath);
        var reservations = JsonConvert.DeserializeObject<List<Reservations>>(text);

        // Find the index of the reservation to remove
        int indexToRemove = reservations.FindIndex(r => r.ID == reservation.ID);

        if (indexToRemove != -1)
        {
            // verwijderd van list
            reservations.RemoveAt(indexToRemove);

            string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);

            File.WriteAllText(jsonfilepath, updatedJson);

            Console.WriteLine($"Reservatie {reservation.ID} is succesvol geannuleerd!");
        }
        else
        {
            Console.WriteLine($"Reservatie {reservation.ID} kon niet worden gevonden om te annuleren.");
        }
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