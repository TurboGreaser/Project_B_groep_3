namespace Project_B;

public class Showing
{
    public DateTime Date;
    public Zaal zaal;

    public Showing(Zaal zaal, DateTime date)
    {
        Date = date;
        this.zaal = zaal;
    }

    public override string ToString()
    {
        // Format the DateTime object
        string formattedDateTime = Date.ToString("yyyy-MM-dd-HH-mm");

        return $"Datum: {formattedDateTime}, Zaal: {zaal}";
    }
}
