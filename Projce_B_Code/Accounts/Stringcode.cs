namespace Project_B;
using System.Text;
public static class Stringcode 
{
    public static string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
    
    public static string Base64Decode(string encodedText)
    {
        byte[] encodedTextBytes = Convert.FromBase64String(encodedText);
        return Encoding.UTF8.GetString(encodedTextBytes);
    }
}