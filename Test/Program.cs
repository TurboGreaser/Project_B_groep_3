using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a Dictionary<int, string> with items using target-typed new
        Dictionary<int, string> test_showing = new()
        {
            { 1, "Value1" },
            { 2, "Value2" },
            { 3, "Value3" }
        };

        // Access and print the items in the dictionary
        foreach (var kvp in test_showing)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
