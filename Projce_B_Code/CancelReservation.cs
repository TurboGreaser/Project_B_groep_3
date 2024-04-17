using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public static class CancelReservation
{
    public static string jsonfilepath = "Reservations.json";
    public static string jsonfilepathfilms = "Films.json";

    public static void InfoFromUser(string emailuser)
    {
        Console.WriteLine("Weet u zeker dat u uw reservering wilt annuleren? (ja/nee)");

        string enteredchoice = Console.ReadLine().ToLower();

        if (enteredchoice == "ja")
        {
            string text = File.ReadAllText(jsonfilepath);
            var all_reservation = JsonConvert.DeserializeObject<List<Reservations>>(text);
            Reservations user_reservations = all_reservation.Find(email_info => email_info.Emails == emailuser);


            if (user_reservations != null)
            {
                Console.WriteLine($"Je hebt gereserveerd op {user_reservations.Date} voor zaal {user_reservations.ZaalID}");

                Console.WriteLine("Vul het zaal nummer in van de reservering die u wilt annuleren");

                int chosenZaal;
                if (int.TryParse(Console.ReadLine(), out chosenZaal))
                {
                    RemoveReservationByRoomID(chosenZaal);
                }
                else
                {
                    Console.WriteLine("Ongeldige invoer voor zaalnummer.");
                }
            }
            else
            {
                Console.WriteLine("Uw email is niet gevonden in reservaties!");
            }
        }
        else
        {
            Console.WriteLine("Uw reservering is niet verwijderd!");
        }
    }

    public static void RemoveReservationByRoomID(int room_Id)
    {
        string text = File.ReadAllText(jsonfilepath);
        var reservation_user = JsonConvert.DeserializeObject<List<Reservations>>(text);

        // Search for the reservation using room ID
        Reservations reservation_remover = reservation_user.Find(room_Id_info => room_Id_info.ZaalID == room_Id);

        if (reservation_remover != null)
        {
            reservation_user.Remove(reservation_remover);

            string jsontext = JsonConvert.SerializeObject(reservation_user, Formatting.Indented);

            File.WriteAllText(jsonfilepath, jsontext);

            Console.WriteLine("Uw reservatie is verwijderd!");
        }
        else
        {
            Console.WriteLine("De zaal is niet gevonden.");
        }
    }

    public static double GetMoviePrice(string movieName)
    {
        string text = File.ReadAllText(jsonfilepathfilms);
        Film films = JsonConvert.DeserializeObject<List<Film>>(text);

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