using Project_B;

class MenuStore
{
    public static List<MenuItem> menuItems;

    public MenuStore()
    {
        menuItems = new List<MenuItem>();
    }

    public void AddItem(string name, double price)
    {
        MenuItem newItem = new MenuItem(name, price);
        menuItems.Add(newItem);
    }

    public static void PrintMenu()
    {
        FormattedOutput.Display_RestaurantMenu();
        Console.WriteLine("Item           Prijs");
        Console.WriteLine("Popcorn Zout     4,50 Euro");
        Console.WriteLine("Popcorn Mix      5,50 Euro");
        Console.WriteLine("Frisdrank        2,00 Euro");
        Console.WriteLine("Nacho's          5,00 Euro");
        Console.WriteLine("Hotdog           3,75 Euro");
        Console.WriteLine("Haribo           1,75 Euro");
        Console.WriteLine("Cola             1,50 Euro");
    
        string a = Console.ReadLine();
        // foreach (MenuItem item in menuItems)
        // {
        //     Console.WriteLine(item);
        // }
    }
}
