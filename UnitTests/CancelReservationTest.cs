using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_B;
using Newtonsoft.Json;
namespace UnitTests

{
    [TestClass]
    public class CancelReservationTests
    {
        private string originalJsonReservations;
        private string originalJsonFilms;

        [TestInitialize]
        public void TestInitialize()
        {
            originalJsonReservations = @"
            [
                {
                    ""ID"": ""An American in Paris|Vincente Minnelli|2022-12-20 15:00|2"",
                    ""Date"": ""2022-12-20 15:00"",
                    ""ZaalID"": 2,
                    ""Seats"": [21, 1, 3],
                    ""Emails"": [""MarkSalloum893@gmail.com"", ""ivan@g.com"", ""ivan@g.com""]
                },
                {
                    ""ID"": ""Film 20|Director 20|2023-02-01 21:45|2"",
                    ""Date"": ""2023-02-01 21:45"",
                    ""ZaalID"": 2,
                    ""Seats"": [31, 34, 4],
                    ""Emails"": [""test@gmail.com"", ""koko1@gmail.com"", ""ivan@g.com""]
                }
            ]";

            originalJsonFilms = @"
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
                }
            ]";

           
            File.WriteAllText(CancelReservation.jsonfilepath, originalJsonReservations);
            File.WriteAllText(CancelReservation.jsonfilepathfilms, originalJsonFilms);
        }

        [TestMethod]
        public void RemoveSeatFromReservation_ValidInput_RemovesSeat()
        {
            // Arrange
            var reservation = new Reservations
            {
                ID = "An American in Paris|Vincente Minnelli|2022-12-20 15:00|2",
                Date = "2022-12-20 15:00",
                ZaalID = 2,
                Seats = new List<int> { 21, 1, 3 },
                Emails = new List<string> { "MarkSalloum893@gmail.com", "ivan@g.com", "ivan@g.com" }
            };
            string email = "ivan@g.com";
            int seatToRemove = 1;

            // Act
            CancelReservation.RemoveSeatFromReservation(reservation, email, seatToRemove);

            // Assert
            var updatedJson = File.ReadAllText(CancelReservation.jsonfilepath);
            var updatedReservations = JsonConvert.DeserializeObject<List<Reservations>>(updatedJson);
            Assert.AreEqual(2, updatedReservations.Count); // Number of reservations should remain the same
            var updatedReservation = updatedReservations.Find(r => r.ID == reservation.ID);
            Assert.IsNotNull(updatedReservation); // Reservation should still exist
            CollectionAssert.DoesNotContain(updatedReservation.Seats, seatToRemove); // Seat should be removed
        }

        [TestMethod]
        public void HandleSeatSelection_SingleSeat_RemovesSeat()
        {
            // Arrange
            var reservation = new Reservations
            {
                ID = "An American in Paris|Vincente Minnelli|2022-12-20 15:00|2",
                Date = "2022-12-20 15:00",
                ZaalID = 2,
                Seats = new List<int> { 21 },
                Emails = new List<string> { "MarkSalloum893@gmail.com" }
            };
            string email = "MarkSalloum893@gmail.com";

            // Mock user input/output
            CancelReservation.ReadLine = () => "1"; // Select the only seat
            CancelReservation.WriteLine = (message) => { }; // Mock WriteLine

            // Act
            CancelReservation.HandleSeatSelection(reservation, email);

            // Assert
            var updatedJson = File.ReadAllText(CancelReservation.jsonfilepath);
            var updatedReservations = JsonConvert.DeserializeObject<List<Reservations>>(updatedJson);
            Assert.AreEqual(1, updatedReservations.Count); // Reservation should be removed
        }

        // Additional test cases can be added to cover more scenarios
    }
}
