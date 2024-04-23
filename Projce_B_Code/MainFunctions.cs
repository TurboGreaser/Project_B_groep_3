namespace Project_B;

public static class MainFunctions
{
    public static void MakeNewReservation(Accounts loggedInAccount)
    {
        List<Film> filmList = JsonReader.ReadFilmJson();
        ListFunctions.Display(filmList);
        Film chosenFilm = MainMenu.Choose(filmList); // function for choosing film


        var chosenDateAndZaal = ListFunctions.ChooseShowing(chosenFilm); // chosenFIlm in here

        string chosenDate = chosenDateAndZaal.First().Key;

        Reservation.MakeReservation(chosenFilm, chosenDate, loggedInAccount.Age, loggedInAccount.Email);
    }
}
