namespace Project_B;

public static class Demo
{
    public static void PlayDemo()
    {
        Console.Clear();
        // // menu laten zien
        MainMenu.ShowMenu();

        Console.ReadLine();
        Console.Clear();

        // // account maken laten zien
        AddAccount.MakeAccount();

        Console.ReadLine();
        Console.Clear();

        // // account deleten laten zien
        RemoveAccount.GetInfo();

        Console.ReadLine();
        Console.Clear();

        // film list sortereen laten zien op naam
        // eerst films.json laten zien
        var films = JsonReader.ReadFilmJson();
        films = ListFunctions.SortList(films, "Name", true);
        ListFunctions.Display(films);

        Console.ReadLine();
        Console.Clear();

        // film sorteren laten zien op duration
        films = ListFunctions.SortList(films, "Duration_in_minutes", true);
        ListFunctions.Display(films);

        Console.ReadLine();
        Console.Clear();

        // film soteren door te typen
        Console.WriteLine("Je kan films zoeken op naam");
        string search = Console.ReadLine()!;
        Console.WriteLine(ListFunctions.Search(films, search));

        Console.ReadLine();
        Console.Clear();

        // restaurant menu laten zien
        MenuStore store = new();
        store.AddItem("Bier", 5.00);
        store.AddItem("Bitterballen", 9.50);
        store.PrintMenu();

        // seat selection laten zien
        SeatSelection.SelectSeat(5);

        SeatSelection.SelectSeat(9);


        Console.ReadLine();
        Console.Clear();

        DigitalCard.DrawSquare("Spider-man ", 1, 10);





    }
}
