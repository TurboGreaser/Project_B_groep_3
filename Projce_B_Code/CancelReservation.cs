using Newtonsoft.Json;


public static class CancelReservation
{
    public static string jsonfilepath = "Reservations.json";
    public static string jsonfilepathaccounts = "Accounts.json";

    public static void InfoFromUser()
    {
        Console.WriteLine("To cancel your reservation, please enter your email and password:");
        Console.WriteLine("What is your email?");
        string enteredemail = Console.ReadLine();
        Console.WriteLine("What is your password?");
        string enteredpassword = Console.ReadLine();

        bool accountExists = CheckAccountByPassAndEmail(enteredemail, enteredpassword);

        if (accountExists)
        {
            Console.WriteLine("You can now cancel your reservation");
            Console.WriteLine("Please enter your ID and the date:");
            Console.WriteLine("What is your ID?");
            string id_reservation = Console.ReadLine();
            Console.WriteLine("What is date and time? (YYYY-MM-DD HH:MM)");
            string date_reservation = Console.ReadLine();
            RemoveReservationByPassAndId(id_reservation, date_reservation);
        }
        else
        {
            Console.WriteLine("Account with the entered Email and Password was not found");
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

            Console.WriteLine("Your reservation has been removed");
        }
        else
        {
            Console.WriteLine("Reservation with the entered ID and date was not found");
        }
    }

    public static bool CheckAccountByPassAndEmail(string email, string password)
    {
        string text = File.ReadAllText(jsonfilepathaccounts);
        var useraccounts = JsonConvert.DeserializeObject<List<Accounts>>(text);

        Accounts userToCheck = useraccounts.Find(person => person.Email == email && person.Password == password);

        return userToCheck != null;
    }
}
