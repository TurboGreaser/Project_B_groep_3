namespace Project_B;

public static class MainFunctions
{
    public static void MakeNewReservation(Accounts loggedInAccount)
    {
        List<Film> filmList = JsonReader.ReadFilmJson();
<<<<<<< Updated upstream
        Film chosenFilm = ListFunctions.ChooseFilm(filmList); // function for choosing film

=======
        ListFunctions.Display(filmList);
        Film chosenFilm = MainMenu.ShowFilmList(filmList); // function for choosing film
        if (chosenFilm == null)
        {
            return;
        }
>>>>>>> Stashed changes

        var chosenDateAndZaal = ListFunctions.ChooseShowing(chosenFilm); // chosenFIlm in here

        string chosenDate = chosenDateAndZaal.First().Key;

        Reservation.MakeReservation(chosenFilm, chosenDate, loggedInAccount.Age, loggedInAccount.Email);
    }
}
