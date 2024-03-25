namespace Project_B;

public class Showing
{
    public DateTime Date;
    public Zaal zaal;

    public Showing(Zaal zaal, string date)
    {
        Date = SetDateTime(date);
        this.zaal = zaal;
    }

    public override string ToString()
    {
        // Format the DateTime object
        string formattedDateTime = Date.ToString("yyyy-MM-dd HH:mm");

        return $"Datum: {formattedDateTime}, Zaal: {zaal.ID}";
    }

    private DateTime SetDateTime(string dateString)
    {
        string format = "yyyy-MM-dd HH:mm";

        // Parse the string to DateTime using ParseExact method
        if (DateTime.TryParseExact(dateString, format, null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
        {
            return dateTime;
        }
        else
        {
            DateTime noDate = new(2000, 1, 1, 1, 1, 1);
            return noDate;
        }
    }
}
