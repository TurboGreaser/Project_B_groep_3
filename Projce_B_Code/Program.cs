﻿using Project_B;

namespace Priject_b;


public class Program
{
    public static void Main()
    {
        // var films = JsonReader.ReadFilmJson();

        // string date = "";
        // foreach (var showing in films[0].Showings)
        // {
        //     date = showing.Key;
        //     break;
        // }

        // Reservation.MakeReservation(films[0], date);
        // List<Film> films = new List<Film>();
        // Console.WriteLine("===Film kiezen===");
        // films.Add(new Film("The Shawshank Redemption", "Drama", 142, 9.3, "Frank Darabont", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", new Dictionary<string, int> { { "April 10, 2024", 1 }, { "April 11, 2024", 2 } }));
        // films.Add(new Film("The Godfather", "Crime", 175, 9.2, "Francis Ford Coppola", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", new Dictionary<string, int> { { "April 10, 2024", 3 }, { "April 11, 2024", 2 } }));
        // films.Add(new Film("The Dark Knight", "Action", 152, 9.0, "Christopher Nolan", "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.", new Dictionary<string, int> { { "April 10, 2024", 2 }, { "April 11, 2024", 3 } }));
        // Film ChosenFilm = ListFunctions.ChooseFilm(films);
        // Console.WriteLine("Je hebt voor deze film gekozen:");
        // Console.WriteLine(ChosenFilm.CompactInfo());
        // ListFunctions.ChooseShowing(ChosenFilm);

        SeatSaleRoom.GetMiddleSeats(20);

    }
}
