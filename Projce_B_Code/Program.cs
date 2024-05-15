
namespace Project_B;

class Program
{
    static void Main()
    {
        // Test reservation cancellation
        // CancelReservation.InfoFromUser("TestEmail1@gmail.com");
        var showings = new List<(string, int)>
        {
            ("Monday", 1200),
            ("Tuesday", 1400),
            ("Wednesday", 1800)
        };

        // Creating a random film object
        Film randomFilm = new Film(
            name: "Random Film",
            genre: "Action",
            duration: 120,
            price: 9.99,
            director: "John Doe",
            description: "A thrilling action film",
            showings: showings
        );
        ListFunctions.ChooseShowing(randomFilm);



    }
}
