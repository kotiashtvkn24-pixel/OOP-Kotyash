using System;
using System.Text;

class House
{
    private string address;
    private string type;

    public int Floors { get; set; }

    public House(string address, string type, int floors)
    {
        this.address = address;
        this.type = type;
        Floors = floors;
    }

    public string GetInfo()
    {
        return $"Адреса: {address}, Тип: {type}, Поверхів: {Floors}";
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        House house1 = new House("вул. Шевченка, 10", "Приватний будинок", 2);
        House house2 = new House("вул. Соборна, 25", "Багатоквартирний будинок", 5);
        House house3 = new House("вул. Київська, 15", "Котедж", 2);

        Console.WriteLine("Інформація про будинки:");
        Console.WriteLine(house1.GetInfo());
        Console.WriteLine(house2.GetInfo());
        Console.WriteLine(house3.GetInfo());
    }
}