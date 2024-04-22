using Newtonsoft.Json;
using Project_B;
//using System;
//using System.Collections.Generic;
//using System.IO;

public static class CancelReservation
{
    public static string jsonfilepath = "Reservations.json";
    public static string jsonfilepathfilms = "Films.json";



    public static void InfoFromUser(string emailToRemove)
    {
        Console.WriteLine("Weet u zeker dat u uw reservering wilt annuleren? (ja/nee)");

        string enteredChoice = Console.ReadLine().ToLower();

        if (enteredChoice == "ja")
        {
            string text = File.ReadAllText(jsonfilepath);
            var reservations = JsonConvert.DeserializeObject<List<Reservations>>(text);

            RemoveReservationByEmail(reservations, emailToRemove);
        }
        else
        {
            Console.WriteLine("Uw reservering is niet verwijderd!");
        }
    }



    public static void RemoveReservationByEmail(List<Reservations> reservations, string emailToRemove)
    {
        foreach (var reservation in reservations)
        {
            int index = reservation.Emails.IndexOf(emailToRemove); // kijkt stoel nummer
            if (index != -1) // stoel gevonden
            {
                Console.WriteLine($"Reservatie info: {reservation.ID}, Zaal: {reservation.ZaalID}");

                Console.WriteLine("Uw reservering wordt geannuleerd!");

                reservation.Emails.RemoveAt(index);
                reservation.Seats.RemoveAt(index);

                string jsontext = JsonConvert.SerializeObject(reservations, Formatting.Indented);
                File.WriteAllText(jsonfilepath, jsontext);

                Console.WriteLine("Uw reservering is geannuleerd!");
                return;
            }
        }

        Console.WriteLine("Uw email is niet gevonden in de reserveringen!");
    }



    public static double GetMoviePrice(string movieName)
    {
        string text = File.ReadAllText(jsonfilepathfilms);
        var films = JsonConvert.DeserializeObject<List<Project_B.Film>>(text);

        Project_B.Film movie = films.Find(film => film.Name == movieName);

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