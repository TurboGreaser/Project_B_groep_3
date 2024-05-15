namespace Project_B;

public static class MainFunctions
{
    public static void MakeNewReservation(Accounts loggedInAccount)
    {
        List<Film> filmList = JsonReader.ReadFilmJson();
        Film chosenFilm = MainMenu.ShowFilmList(MainMenu.SortedFilm); // function for choosing film
        if (chosenFilm == null)
        {
            return;
        }

        var chosenDateAndZaal = ListFunctions.ChooseShowing(chosenFilm); // chosenFIlm in here

        string chosenDate = chosenDateAndZaal.Item1;

        Reservation.MakeReservation(chosenFilm, chosenDate, loggedInAccount.Age, loggedInAccount.Email);
    }
}
