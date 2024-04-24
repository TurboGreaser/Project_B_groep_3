namespace Project_B;
class ConfirmationMessage
{
    public static void ShowConfirmationMessage(Film film, string date, string email, int total_price)
    {
        Console.WriteLine("Uw reservering is succesvol geplaatst.");
        Console.WriteLine($"Film: {film.Name}");
        Console.WriteLine($"Zaal: {date}");
        Console.WriteLine($"Email: {email}");
        Console.WriteLine($"Prijs: {total_price} euro");
    }
}