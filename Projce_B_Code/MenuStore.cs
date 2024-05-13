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
        Console.WriteLine("----- BIOSCOOP MENU -----");
        Console.WriteLine("Item           Prijs");
        Console.WriteLine("Popcorn Zout     4,50 Euro");
        Console.WriteLine("Popcorn Mix      5,50 Euro");
        Console.WriteLine("Frisdrank        2,00 Euro");
        Console.WriteLine("Nacho's          5,00 Euro");
        Console.WriteLine("Hotdog           3,75 Euro");
        Console.WriteLine("Haribo           1,75 Euro");
        foreach (MenuItem item in menuItems)
        {
            Console.WriteLine(item);
        }
    }
}
