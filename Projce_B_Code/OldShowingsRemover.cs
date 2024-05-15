namespace Project_B;

using System.Globalization;

public static class OldShowingsRemover
{
    public static void RemoveShowingsFromMovies(DateTime CurrentTime = default, string fileName = "Films.json")
    {
        if (CurrentTime == default)
        { CurrentTime = DateTime.Now; }

        string format = "yyyy-MM-dd HH:mm";

        List<Film> films = JsonReader.ReadFilmJson(fileName);



        // loop trought films in reverse and remove the showings that have passed
        foreach (Film film in films)
        {
            List<(string,int)> newShowings = new();
            foreach (var showing in film.Showings)
            {
                // this line turns the datetime string into a datetime obj
                DateTime showingTime = DateTime.ParseExact(showing.Item1, format, CultureInfo.InvariantCulture);

                // showing gets added to the now List if its not past the current time
                if (showingTime > CurrentTime)
                {
                    newShowings.Add(showing);
                }
            }
            // update shwoings
            film.Showings = newShowings;
        }
        // write the films with updated showings at the end
        Json_writer.WriteFilmToJSON(films, fileName);
    }

    public static void RemoveOldReservations(DateTime CurrentTime = default, string fileName = "Films.json")
    {
        if (CurrentTime == default)
        { CurrentTime = DateTime.Now; }

        string format = "yyyy-MM-dd HH:mm";

        List<Json_writer.ReservationJsonObj> reservations = JsonReader.ReadReservations(fileName);

        for (int i = reservations.Count - 1; i >= 0; i--)
        {
            Json_writer.ReservationJsonObj reservation = reservations[i];
            DateTime reservationTime = DateTime.ParseExact(reservation.Date, format, CultureInfo.InvariantCulture);
            if (reservationTime < CurrentTime)
            {
                reservations.RemoveAt(i);
            }
        }
        Json_writer.WriteReservationsToJSON(reservations, fileName);
    }
}
