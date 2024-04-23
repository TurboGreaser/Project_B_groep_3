public class Reservations
{
    public string ID { get; set; }
    public string Date { get; set; }
    public int ZaalID { get; set; }
    public List<int> Seats { get; set; }
    public List<string> Emails { get; set; }
}
