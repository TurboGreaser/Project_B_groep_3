namespace UnitTests
{
    using Project_B;

    [TestClass]
    public class SortFunctionTest
    {
        private List<Film> InitializeFilms()
        {
            return new List<Film>
            {
                new Film("Inception", "Science Fiction", 148, 12.99, "Christopher Nolan", "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    new List<Showing>
                    {
                        new Showing("2024-06-05 14:00", 1),
                        new Showing("2024-06-05 18:00", 2),
                        new Showing("2024-06-06 20:00", 1)
                    }),
                new Film("The Matrix", "Action", 136, 9.99, "Lana Wachowski, Lilly Wachowski", "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                    new List<Showing>
                    {
                        new Showing("2024-06-05 16:00", 3),
                        new Showing("2024-06-06 19:00", 2),
                        new Showing("2024-06-07 22:00", 1)
                    }),
                new Film("Interstellar", "Adventure", 169, 14.99, "Christopher Nolan", "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                    new List<Showing>
                    {
                        new Showing("2024-06-05 17:00", 4),
                        new Showing("2024-06-06 21:00", 3),
                        new Showing("2024-06-07 19:00", 2)
                    }),
                new Film("The Godfather", "Crime", 175, 10.99, "Francis Ford Coppola", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    new List<Showing>
                    {
                        new Showing("2024-06-05 19:00", 5),
                        new Showing("2024-06-06 15:00", 1),
                        new Showing("2024-06-07 20:00", 4)
                    }),
                new Film("Pulp Fiction", "Drama", 154, 11.99, "Quentin Tarantino", "The lives of two mob hitmen, a boxer, a gangster, and his wife intertwine in four tales of violence and redemption.",
                    new List<Showing>
                    {
                        new Showing("2024-06-05 20:00", 2),
                        new Showing("2024-06-06 18:00", 5),
                        new Showing("2024-06-07 21:00", 3)
                    })
            };
        }

        [DataTestMethod]
        [DataRow("genre", false, "Inception,Pulp Fiction,The Godfather,Interstellar,The Matrix")]
        [DataRow("duur", true, "The Matrix,Inception,Pulp Fiction,Interstellar,The Godfather")]
        [DataRow("prijs", true, "The Matrix,The Godfather,Pulp Fiction,Inception,Interstellar")]
        [DataRow("naam", true, "Inception,Interstellar,Pulp Fiction,The Godfather,The Matrix")]
        public void SortList_Test(string orderBy, bool asc, string expectedOrder)
        {
            List<Film> films = InitializeFilms();
            List<string> expected = expectedOrder.Split(',').ToList();

            List<string> actual = ListFunctions.SortList(films, orderBy, asc).Select(film => film.Name).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
