using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
namespace UnitTests;
using Project_B;

[TestClass]
public class SaleMovieTests
{
    private const string FilmsJson = @"
    [
        {
            ""Name"": ""12 Angry Men"",
            ""Genre"": ""Drama"",
            ""Duration_in_minutes"": 96,
            ""Price"": 10.5,
            ""Director"": ""Sidney Lumet"",
            ""Description"": ""A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence."",
            ""Showings"": [
                {
                    ""Datum"": ""2022-12-30 14:00"",
                    ""Zaal"": 1
                },
                {
                    ""Datum"": ""2024-11-25 18:00"",
                    ""Zaal"": 2
                },
                {
                    ""Datum"": ""2024-12-25 20:00"",
                    ""Zaal"": 3
                }
            ]
        },
        {
            ""Name"": ""Lawrence of Arabia"",
            ""Genre"": ""Adventure/Drama"",
            ""Duration_in_minutes"": 216,
            ""Price"": 13.0,
            ""Director"": ""David Lean"",
            ""Description"": ""The story of T.E. Lawrence, the English officer who successfully united and led the diverse, often warring, Arab tribes during World War I in order to fight the Turks."",
            ""Showings"": [
                {
                    ""Datum"": ""2022-12-31 17:00"",
                    ""Zaal"": 3
                },
                {
                    ""Datum"": ""2024-11-26 13:00"",
                    ""Zaal"": 1
                },
                {
                    ""Datum"": ""2024-12-26 15:00"",
                    ""Zaal"": 2
                }
            ]
        },
        {
            ""Name"": ""Dr. Strangelove"",
            ""Genre"": ""Comedy/Drama"",
            ""Duration_in_minutes"": 95,
            ""Price"": 9.5,
            ""Director"": ""Stanley Kubrick"",
            ""Description"": ""An insane general triggers a path to nuclear holocaust that a war room full of politicians and generals frantically try to stop."",
            ""Showings"": [
                {
                    ""Datum"": ""2022-12-29 20:00"",
                    ""Zaal"": 2
                },
                {
                    ""Datum"": ""2024-11-24 21:00"",
                    ""Zaal"": 1
                },
                {
                    ""Datum"": ""2024-12-24 19:00"",
                    ""Zaal"": 3
                }
            ]
        }
    ]";


    [TestMethod]
    public void GetSaleDetails_DiscountApplied()
    {
        // Arrange
        string movieName = "12 Angry Men";
        DateTime currentTime = new DateTime(2024, 11, 25, 15, 0, 0); // 3 hours before 18:00 showing

        // Act
        double result = SaleMovie.GetSaleDetails(movieName, currentTime);

        // Assert
        Assert.AreEqual(8.40, result); // 20% discount on 10.5 is 8.4
    }

    [TestMethod]
    public void GetSaleDetails_NoDiscount()
    {
        // Arrange
        string movieName = "12 Angry Men";
        DateTime currentTime = new DateTime(2024, 11, 25, 12, 0, 0); // Not within 3 hours before any showing

        // Act
        double result = SaleMovie.GetSaleDetails(movieName, currentTime);

        // Assert
        Assert.AreEqual(10.5, result); // No discount applied
    }

    [TestMethod]
    public void GetSaleDetails_MovieNotFound()
    {
        // Arrange
        string movieName = "Nonexistent Movie";
        DateTime currentTime = new DateTime(2024, 11, 25, 15, 0, 0);

        // Act
        double result = SaleMovie.GetSaleDetails(movieName, currentTime);

        // Assert
        Assert.AreEqual(0, result); // Movie not found
    }
}

