namespace UnitTests;
using Project_B;



[TestClass]
public class MoviesUnitTest
{
    [TestMethod]

    public void TestMovies()
    {
        string TestNameFile = "UnitTestFilms.json";

        if (File.Exists(TestNameFile))
            File.Delete(TestNameFile);

        // create 2 films
        Film film1 = new Film("test_name_1", "test_genre_1", 90, 20.50, "test_director_1", "test_description_1",
        new List<Showing> { new Showing("2020-12-20 12:00", 1), new Showing("2021-12-30 12:00", 2) });


        Film film2 = new Film("test_name_2", "test_genre_2", 90, 20.50, "test_director_2", "test_description_2",
        new List<Showing> { new Showing("2040-12-30 12:00", 1), new Showing("2000-12-30 12:00", 2) });


        Film film3 = new Film("test_name_3", "test_genre_1", 90, 20.50, "test_director_1", "test_description_1",
        new List<Showing> { new Showing("2020-12-20 18:00", 1), new Showing("2021-12-30 12:00", 2) });


        // put the films in  a list
        List<Film> filmsToWrite = new() { film1, film2, film3 };

        // write the films to a test json
        Json_writer.WriteFilmToJSON(filmsToWrite, TestNameFile);

        // add testtime
        DateTime newtime = new DateTime(2020, 12, 20);

        // Movies.ShowMoviesToday(newtime, TestNameFile);
        List<Film> films = Movies.ShowMoviesToday(newtime, TestNameFile);

        Assert.IsTrue(films[0].Name == film1.Name);
        Assert.IsTrue(films[1].Name == film3.Name);

        File.Delete(TestNameFile);

    }
}