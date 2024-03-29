class MenuStore
{
    public List<MenuItem> menuItems;

    public MenuStore()
    {
        menuItems = new List<MenuItem>();
    }

    public void AddItem(string name, double price)
    {
        MenuItem newItem = new MenuItem(name, price);
        menuItems.Add(newItem);
    }

    public void PrintMenu()
    {
        Console.WriteLine("----- BIOSCOOP MENU -----");
        Console.WriteLine("Item           Prijs");
        foreach (MenuItem item in menuItems)
        {
            Console.WriteLine(item);
        }
    }
}
