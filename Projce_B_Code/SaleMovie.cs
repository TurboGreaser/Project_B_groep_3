public class SaleMovie
{
    private const int SalePercent = 20;
    public double PriceForMovie { get; set; }

    public SaleMovie(double priceForMovie)
    {
        PriceForMovie = priceForMovie;
    }

    public double CalculateSale(double priceForMovie)
    {
        double discountAmount = priceForMovie * SalePercent / 100; // berekent korting
        return priceForMovie - discountAmount; // geeft de prijs terug

        // double testprijs = 5; 
        // double korting_test_prijs = SaleMovie.CalculateSale(testprijs);
    }
}
