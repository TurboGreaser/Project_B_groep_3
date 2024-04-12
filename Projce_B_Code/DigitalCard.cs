namespace Project_B;

public class DigitalCard
{
    private static int lastSequence = 0; // Eerste getal nooit hetzelfde
    private static Random random = new Random();

    private static int SizeCard = 20; // Vierkant grootte

    public string MovieName;
    public int RoomNumber;
    public int SeatNumber;

    public DigitalCard(string movieName, int roomNumber, int seatNumber)
    {
        this.MovieName = movieName;
        this.RoomNumber = roomNumber;
        this.SeatNumber = seatNumber;
    }

    public static string UniqueId()
    {
        int sequencePart = ++lastSequence; 
        int randomPart = random.Next(1, 100001);

        return $"{sequencePart}-{randomPart}";
    }

    public static void DrawSquare(string MovieName, int RoomNumber, int SeatNumber)
    {
        // Bovenkant
        Console.WriteLine(new string('-', SizeCard * 2));

        Console.Write("| ");// Linkerkant van retrocinema
        Console.Write("Retro Cinema".PadRight(SizeCard * 2 - 4)); 
        Console.WriteLine("|"); // Rechterkant van retrocinema

        Console.Write("| ");
        Console.Write(" ".PadRight(SizeCard * 2 - 4)); // lege lijn
        Console.WriteLine("|");



        // Movie naam
        Console.Write("| "); // Linkerkant van movie
        Console.Write($"Film: {MovieName}".PadRight(SizeCard * 2 - 4)); 
        Console.WriteLine("|"); // Rechterkant van movie

        // Room nummer
        Console.Write("| "); 
        Console.Write($"Zaal: {RoomNumber}".PadRight(SizeCard * 2 - 4)); 
        Console.WriteLine("|");

        // Stoel nummber
        Console.Write("| ");
        Console.Write($"Stoel: {SeatNumber}".PadRight(SizeCard * 2 - 4)); 
        Console.WriteLine("|");

        // Unique code
        Console.Write("| ");
        Console.Write($"Code: {UniqueId()}".PadRight(SizeCard * 2 - 4)); 
        Console.WriteLine("|");

        // Onderkant
        Console.WriteLine(new string('-', SizeCard * 2));

        // DigitalCard.DrawSquare("Movie ", 1, 10); // voorbeeld om te testen
    }
}


    
