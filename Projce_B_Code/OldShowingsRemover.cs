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
            Dictionary<string, int> newShowings = new();
            foreach (var showing in film.Showings)
            {
                // this line turns the datetime string into a datetime obj
                DateTime showingTime = DateTime.ParseExact(showing.Key, format, CultureInfo.InvariantCulture);

                // showing gets added to the now dict if its not past the current time
                if (showingTime > CurrentTime)
                {
                    newShowings.Add(showing.Key, showing.Value);
                }
            }
            // update shwoings
            film.Showings = newShowings;
        }
        // write the films with updated showings at the end
        Json_writer.WriteFilmToJSON(films, fileName);
    }  
}
