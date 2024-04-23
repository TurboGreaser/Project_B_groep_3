namespace MyNamespace
{
    public class MenuData
    {
        public List<MenuItemData> MenuItems { get; set; }
    }

    public class MenuItemData
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

