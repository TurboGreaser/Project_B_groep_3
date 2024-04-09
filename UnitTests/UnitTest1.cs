namespace UnitTests;
using Project_B;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestOldShowingsRemover()
    {

        string testFilename = "UnitTestFilms.json";

        // create 2 films
        Film film1 = new Film("test_name_1", "test_genre_1", 90, 20.50, "test_director_1", "test_description_1",
        new Dictionary<string, int> { { "2020-12-20 12:00", 1 }, { "2021-12-30 12:00", 2 } });

        Film film2 = new Film("test_name_2", "test_genre_2", 90, 20.50, "test_director_2", "test_description_2",
        new Dictionary<string, int> { { "2040-12-30 12:00", 1 }, { "2000-12-30 12:00", 2 } });

        // put the films in  a list
        List<Film> filmsToWrite = new() { film1, film2 };

        // write the films to a test json
        Json_writer.WriteFilmToJSON(filmsToWrite, testFilename);

        // make a datetime with 2020-12-21 12:00 as the time
        DateTime testTime = new(2020, 12, 21, 12, 0, 0);

        // run the remover function
        OldShowingsRemover.RemoveShowingsFromMovies(testTime, testFilename);

        // read the json
        List<Film> films = JsonReader.ReadFilmJson(testFilename);

        // assert the correct shwoings were removed by checkong the keys and vals
        foreach (var kvp in films[0].Showings)
        {
        Assert.AreEqual(kvp.Key, "2021-12-30 12:00");
        Assert.AreEqual(kvp.Value, 2);
        }

        foreach (var kvp in films[1].Showings)
        {
            Assert.AreEqual(kvp.Key, "2040-12-30 12:00");
            Assert.AreEqual(kvp.Value, 1);
        }
    }
}