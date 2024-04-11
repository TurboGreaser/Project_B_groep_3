using Newtonsoft.Json;
public static class CancelReservation
{
    public static string jsonfilepath = "Reservations.json";
    public static void InfoFromUser()
    {
        Console.WriteLine("To cancel you reservation you have to enter your ID, and the date");    
        Console.WriteLine("What is your ID?");
        string id_reservation = Console.ReadLine();
        Console.WriteLine("What is date and time? (YYYY-MM-DD HH:MM)");
        string date_reservation = Console.ReadLine();


        //Roep de removeAccount method aan
        RemoveReservationByPassAndId(id_reservation, date_reservation);
    }
    public static void RemoveReservationByPassAndId(string id, string date)
    {
        string text = File.ReadAllText(jsonfilepath);
        var reservation_user = JsonConvert.DeserializeObject<List<Reservations>>(text);

        //zoekt naar de id/wachtwoord 
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
}