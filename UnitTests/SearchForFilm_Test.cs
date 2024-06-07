namespace UnitTests;
using Project_B;

[TestClass]
public class SearchForFilm
{
    [TestMethod]
    public void SearchForFilm_WithValidInput()
    {
        var input = new StringReader("Inception");
        Console.SetIn(input);

        var filmList = new List<Film>
        {
            new Film("The Matrix", "Action", 136, 9.99, "The Wachowskis", "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", null),
            new Film("Inception", "Sci-Fi", 148, 11.99, "Christopher Nolan", "A thief who enters the dreams of others to steal their secrets.", null),
            new Film("The Shawshank Redemption", "Drama", 142, 8.99, "Frank Darabont", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", null)
        };

        var result = MainMenu.SearchForFilm(filmList);
        CollectionAssert.AreEqual(filmList.Where(f => f.Name == "Inception").ToList(), result);
    }

    [TestMethod]
    public void SearchForFilm_PartOfTheTitle()
    {
        var input = new StringReader("the");
        Console.SetIn(input);

        List<Film> filmList = new List<Film>
        {
            new Film("The Matrix", "Action", 136, 9.99, "The Wachowskis", "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", null),
            new Film("Inception", "Sci-Fi", 148, 11.99, "Christopher Nolan", "A thief who enters the dreams of others to steal their secrets.", null),
            new Film("The Shawshank Redemption", "Drama", 142, 8.99, "Frank Darabont", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", null)
        };

        List<Film>? result = MainMenu.SearchForFilm(filmList);
        CollectionAssert.AreEqual(filmList.Where(f => f.Name.StartsWith("Th")).ToList(), result);

    }

    [TestMethod]
    public void SearchForFilm_WithInvalidInput_RetriesUntilValidInputProvided()
    {
        var input = new StringReader("InvalidInput"); // Simulate invalid input followed by valid input
        Console.SetIn(input);

        var filmList = new List<Film>
        {
            new Film("The Matrix", "Action", 136, 9.99, "The Wachowskis", "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", null),
            new Film("Inception", "Sci-Fi", 148, 11.99, "Christopher Nolan", "A thief who enters the dreams of others to steal their secrets.", null),
            new Film("The Shawshank Redemption", "Drama", 142, 8.99, "Frank Darabont", "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", null)
        };
        var result = MainMenu.SearchForFilm(filmList);
        Assert.AreEqual(result.Count, 0);
    }
}
