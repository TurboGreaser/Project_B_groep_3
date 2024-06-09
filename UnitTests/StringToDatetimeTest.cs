namespace UnitTests;
using Project_B;


[TestClass]
public class StringToDatetimeTest
{
    [TestMethod]
    public void TeststringToDateTime()
    {
        // string format = "yyyy-MM-dd HH:mm";
        Assert.AreEqual(new DateTime(2000, 12, 21, 12, 30, 0), Film.StringToDatetime("2000-12-21 12:30"));
        Assert.AreEqual(new DateTime(2010, 5, 11, 11, 30, 0), Film.StringToDatetime("2010-05-11 11:30"));
        Assert.AreEqual(new DateTime(2020, 11, 21, 12, 30, 0), Film.StringToDatetime("2020-11-21 12:30"));
    }
}
