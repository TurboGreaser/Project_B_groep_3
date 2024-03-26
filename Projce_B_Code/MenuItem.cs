class MenuItem
{
    public string Name { get; }
    public double Price { get; }

    public MenuItem(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name,-15} {Price,5:F2} Euro";
    }
}
