namespace UnitTests;
using Project_B;


[TestClass]
public class EmailParserTest
{
    [TestMethod]
    [DataRow("valid.email@example.com", true)]
    [DataRow("invalid-email-example.com", false)]
    [DataRow("another.valid.email@example.co.uk", true)]
    [DataRow("invalid-email@com", false)]
    [DataRow("yet.another.valid-email@subdomain.example.org", true)]
    public void TestEmailParser(string email, bool expected)
    {
        Assert.AreEqual(expected, AddAccount.ParseEmail(email));
    }
}
