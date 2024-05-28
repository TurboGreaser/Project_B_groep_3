namespace UnitTests;
using Project_B;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestOldShowingsRemover()
    {

        string TestFileName = "UnitTestFilms.json";

        if (File.Exists(TestFileName))
            File.Delete(TestFileName);

        // create 2 films
        Film film1 = new Film("test_name_1", "test_genre_1", 90, 20.50, "test_director_1", "test_description_1",
        new List<Showing> { new Showing("2020-12-20 12:00", 1), new Showing("2021-12-30 12:00", 2) });

        Film film2 = new Film("test_name_2", "test_genre_2", 90, 20.50, "test_director_2", "test_description_2",
        new List<Showing> { new Showing("2040-12-30 12:00", 1), new Showing("2000-12-30 12:00", 2) });

        // put the films in  a list
        List<Film> filmsToWrite = new() { film1, film2 };

        // write the films to a test json
        Json_writer.WriteFilmToJSON(filmsToWrite, TestFileName);

        // make a datetime with 2020-12-21 12:00 as the time
        DateTime testTime = new(2020, 12, 21, 12, 0, 0);

        // run the remover function
        OldShowingsRemover.RemoveShowingsFromMovies(testTime, TestFileName);

        // read the json
        List<Film> films = JsonReader.ReadFilmJson(TestFileName);

        // assert the correct shwoings were removed by checkong the keys and vals
        foreach (Showing showing in films[0].Showings)
        {
            Assert.AreEqual(showing.Datum, "2021-12-30 12:00");
            Assert.AreEqual(showing.Zaal, 2);
        }

        foreach (Showing showing in films[1].Showings)
        {
            Assert.AreEqual(showing.Datum, "2040-12-30 12:00");
            Assert.AreEqual(showing.Zaal, 1);
        }

        // delete the file at the end for consistency
        File.Delete(TestFileName);
    }

    [TestMethod]
    public void TestOldReservationsRemover()
    {
        string TestFileName = "UnitTestReservations.json";

        if (File.Exists(TestFileName))
            File.Delete(TestFileName);

        // create 2 films
        Film film1 = new Film("test_name_1", "test_genre_1", 90, 20.50, "test_director_1", "test_description_1",
        new List<Showing> { new Showing("2020-12-20 12:00", 1), new Showing("2021-12-30 12:00", 2) });


        Film film2 = new Film("test_name_2", "test_genre_2", 90, 20.50, "test_director_2", "test_description_2",
        new List<Showing> { new Showing("2040-12-30 12:00", 1), new Showing("2000-12-30 12:00", 2) });

        // create a zaal obj
        Zaal test_zaal = new Zaal(1, 10);

        // write the reservations to a test json
        Json_writer.WriteReservationToJSON(film1, test_zaal, "2020-12-20 12:00", 18, TestFileName, new List<int> { 1 }); // will get removed
        Json_writer.WriteReservationToJSON(film1, test_zaal, "2021-12-30 12:00", 18, TestFileName, new List<int> { 2 });
        Json_writer.WriteReservationToJSON(film2, test_zaal, "2040-12-30 12:00", 18, TestFileName, new List<int> { 3 });
        Json_writer.WriteReservationToJSON(film2, test_zaal, "2000-12-30 12:00", 18, TestFileName, new List<int> { 4 }); // will get removed

        // make a datetime with 2020-12-21 12:00 as the time
        DateTime testTime = new(2020, 12, 21, 12, 0, 0);

        // run the remover function
        OldShowingsRemover.RemoveOldReservations(testTime, TestFileName);

        // read the file
        List<Json_writer.ReservationJsonObj> reservations = JsonReader.ReadReservations(TestFileName);

        // assert the lenght of the reservation list and if the dates are correct
        Assert.IsTrue(reservations.Count == 2);
        Assert.AreEqual(reservations[0].Date, "2021-12-30 12:00");
        Assert.AreEqual(reservations[1].Date, "2040-12-30 12:00");

        // delete the file at the end for consistency
        File.Delete(TestFileName);
    }
}