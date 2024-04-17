using Project_B;

namespace Priject_b;


public class Program
{
    public static void Main()
    {
        Accounts account = new()
        {
            Username = "test_user_name",
            Email = "MyTestEmail.com",
            Age = 17,
            Password = "12345"
        };
        MainFunctions.MakeNewReservation(account);
    }
}
