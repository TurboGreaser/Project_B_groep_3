using Newtonsoft.Json; 
namespace Project_B;

public static class CancelReservation
{
    public static string jsonfilepath = "Reservations.json";
    public static string jsonfilepathaccounts = "Accounts.json";
    public static string jsonfilepathfilms = "Films.json";

    public static void InfoFromUser()
    {
        Console.WriteLine("Om uw reservering te annuleren, vul dan de volgende gegevens in:");
        Console.WriteLine("Wat is uw email?");
        string enteredemail = Console.ReadLine();
        Console.WriteLine("Wat is uw wachtwoord?");
        string enteredpassword = Console.ReadLine();

        bool accountExists = CheckAccountByPassAndEmail(enteredemail, enteredpassword);

        if (accountExists)// checkt of de account bestaat zoniet dan reservatie niet door
        {
            bool confirmed = false;

            while (!confirmed)// kijken hvl geld film kost
            {
                Console.WriteLine("Wat is de naam van het film?");
                string movieName = Console.ReadLine();

                double moviePrice = GetMoviePrice(movieName);

                if (moviePrice > 0) // kijken of user eens met geld
                {
                    Console.WriteLine($"Geld dat u terug krijgt: {moviePrice}");
                    Console.WriteLine("Klopt dit? (ja/nee)");
                    string confirmation = Console.ReadLine().ToLower();

                    if (confirmation == "ja")
                    {
                        confirmed = true;
                    }
                }
                else
                {
                    Console.WriteLine("Film niet gevonden. Vergeet niet de hoofdletters!");
                }
            }

            Console.WriteLine(" ");
            Console.WriteLine("Om uw reservering te annuleren, vul uw reserverings ID en de datum in:");
            Console.WriteLine("Wat is uw reserverings ID?");
            string id_reservation = Console.ReadLine();
            Console.WriteLine("Wat is de datum van de film? (YYYY-MM-DD HH:MM)");
            string date_reservation = Console.ReadLine();
            RemoveReservationByPassAndId(id_reservation, date_reservation);
        }
        else
        {
            Console.WriteLine("Account niet gevonden!");
        }
    }

    public static void RemoveReservationByPassAndId(string id, string date)
    {
        string text = File.ReadAllText(jsonfilepath);
        var reservation_user = JsonConvert.DeserializeObject<List<Reservations>>(text);

        // Search for the reservation using ID and date
        Reservations reservation_remover = reservation_user.Find(id_info => id_info.ID == id && id_info.Date == date);

        if (reservation_remover != null)
        {
            reservation_user.Remove(reservation_remover);

            string jsontext = JsonConvert.SerializeObject(reservation_user, Formatting.Indented);

            File.WriteAllText(jsonfilepath, jsontext);

            Console.WriteLine("Uw reservatie is compleet!");
        }
        else
        {
            Console.WriteLine("Uw reservatie is niet gevonden!.");
        }
    }

    public static bool CheckAccountByPassAndEmail(string email, string password)
    {
        string text = File.ReadAllText(jsonfilepathaccounts);
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        Accounts userToCheck = useraccounts.Find(person => person.Email == email && person.Password == password);

        return userToCheck != null;
    }

    public static double GetMoviePrice(string movieName)
    {
        string text = File.ReadAllText(jsonfilepathfilms);
        var films = JsonConvert.DeserializeObject<List<Film>>(text);

        Film movie = films.Find(film => film.Name == movieName); // film zoeken

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
