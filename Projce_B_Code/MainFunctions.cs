namespace Project_B;

public static class MainFunctions
{
    public static void MakeNewReservation(Accounts loggedInAccount)
    {
        Film chosenFilm = MainMenu.ShowFilmList(MainMenu.SortedFilm); // function for choosing film
        if (chosenFilm == null)
        {
            return;
        }

        Showing ChosenShowing = ListFunctions.ChooseShowing(chosenFilm); // chosenFIlm in here

        string chosenDate = ChosenShowing.Datum;

        Reservation.MakeReservation(chosenFilm, chosenDate, loggedInAccount.Age, loggedInAccount.Email);
    }
}
