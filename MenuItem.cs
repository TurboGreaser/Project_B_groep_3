class MenuItem
{
    public string Name { get; }
    public decimal Price { get; }

    public MenuItem(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Name,-15} {Price,5:F2} Euro";
    }
}
