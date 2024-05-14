namespace Project_B;

using System.Text;

public class StatisticsManager
{
    public static void AddPaymentDatapoint(double amountInEuro, Film film, int seatsCount, string date, int age)
    {
        string fileName = "SaleData.csv";
        List<string> lines = new();

        if (File.Exists(fileName))
        {
            lines = new List<string>(File.ReadAllLines(fileName));
        }
        StreamWriter writer = new StreamWriter(fileName, true);


        List<string> header = new() { "Date", "FilmName", "FilmGenre", "FilmDurationInMinutes", "AmountSpent", "SeatCount", "Age" };

        if (lines.Count == 0)
        {
            writer.WriteLine(string.Join(",", header));
        }

        List<string> newData = new() { date, film.Name, film.Genre, Convert.ToString(film.Duration_in_minutes), Convert.ToString(amountInEuro), Convert.ToString(seatsCount), age == 999 ? "null" : Convert.ToString(age) };
        for (int i = 0; i < newData.Count; i++)
        {
            if (newData[i] != null)
            {
                newData[i] = newData[i].Replace(",", " ");
            }
        }
        writer.WriteLine(string.Join(",", newData));
        writer.Close();
        writer.Dispose();
    }
}
