namespace Project_B;

public class MainFunctions
{
    public void MakeNewReservation(Accounts loggedInAccount)
    {
        Film chosenFilm = null; // function for choosing film


        string chosenDate = null; // chosenFIlm in here

        // function for calculating cost

        Reservation.MakeReservation(chosenFilm, chosenDate, loggedInAccount.Email);
    }
}
