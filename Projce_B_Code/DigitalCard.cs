using Newtonsoft.Json;
using System.io;

public class DigitalCard
{
        private static int lastSequence = 0;
        private static Random random = new Random();

        public static string UniqueId()
        {
            int sequencePart = ++lastSequence; // Zorgt dat 1e getal nooit gelijk is
            int randomPart = random.Next(1, 100001); 

            return $"{sequencePart}-{randomPart}";
        }

}