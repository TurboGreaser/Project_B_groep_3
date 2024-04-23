namespace Project_B;

public static class Reservation
{
    public static void MakeReservation(Film film, string date, int age, string email)
    {
        Zaal zaal = GetZaalFromFilm(film, date);
        Console.Clear();
        Console.WriteLine($"Je maakt een reservering voor: {film.Name} door: {film.Director}");
        Console.WriteLine($"Op {date} in zaal {zaal.ID}");
        Console.WriteLine($"klik Enter om veder te gaan naar het kiezen van je stoel");
        Console.ReadLine();


        Json_writer.WriteReservationToJSON(film, zaal, date, age, email: email);

    }

    private static Zaal GetZaalFromFilm(Film film, string date)
    {
        List<Zaal> zalen = JsonReader.ReadZalen();



        foreach (Zaal zaal in zalen)
        {
            foreach (var showing in film.Showings)
            {
                if (showing.Key == date && showing.Value == zaal.ID)
                {
                    return zaal;
                }
            }
        }

        Console.WriteLine("Film heeft geen Showings Default zaal wordt gebruikt");
        return zalen[0];
    }

    public static bool PrintPrice(double basePrice, double seatFee, double ageFee)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("De totale prijs is");
            Console.WriteLine($"Film: {basePrice}");
            Console.WriteLine($"Luxe Zitplaats: {seatFee}");
            Console.WriteLine($"onder 18 tarief: {ageFee}");
            Console.WriteLine($"------------------------------------------ +");
            Console.WriteLine($"Totaal: {basePrice + seatFee + ageFee}");


            Console.WriteLine($"Betalen met 1: Ideal, 2 Op locatie, 3 Resevatie annuleren");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine($"Ideal Betaling van {basePrice + seatFee + ageFee} Euro sucsess!");
                Console.WriteLine($"Klik enter om veder te gaaan");
                Console.ReadLine();
                return true;

            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine($"Betaal: {basePrice + seatFee + ageFee} Euro aan de balie ");
                Console.WriteLine($"Klik enter om veder te gaaan");
                Console.ReadLine();
                return true;
            }
            else if (choice == "3")
            {
                Console.Clear();
                Console.WriteLine($"Reservering geanuleerd.");
                Console.WriteLine($"Klik enter Terug te gaan naar het menu");
                return false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Kies 1, 2 of 3");
                Console.WriteLine($"Klik enter om veder te gaaan");
                Console.ReadLine();
            }
        }
    }
}


