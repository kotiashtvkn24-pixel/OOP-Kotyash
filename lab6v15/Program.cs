using System;

class Clothing
{
    private string _material;
    private string _size;

    public string Material
    {
        get { return _material; }
        set { _material = value; }
    }

    public string Size
    {
        get { return _size; }
        set { _size = value; }
    }

    public Clothing(string material, string size)
    {
        _material = material;
        _size = size;
    }

    public virtual void Wear()
    {
        Console.WriteLine($"Одяг ({Material}, розмір {Size}) вдягнено");
    }

    public string GetClothingType()
    {
        return "Одяг";
    }
}

class Shirt : Clothing
{
    public int SleeveLength { get; set; }

    public Shirt(string material, string size, int sleeveLength) : base(material, size)
    {
        SleeveLength = sleeveLength;
    }

    public override void Wear()
    {
        Console.WriteLine($"Сорочку ({Material}, розмір {Size}, рукав {SleeveLength} см) вдягнено");
    }

    public void ButtonUp()
    {
        Console.WriteLine("Сорочку застебнуто на ґудзики");
    }

    public new string GetClothingType()
    {
        return "Сорочка";
    }
}

class Pants : Clothing
{
    public int WaistSize { get; set; }

    public Pants(string material, string size, int waistSize) : base(material, size)
    {
        WaistSize = waistSize;
    }

    public override void Wear()
    {
        Console.WriteLine($"Штани ({Material}, розмір {Size}, талія {WaistSize} см) вдягнено");
    }

    public void ZipUp()
    {
        Console.WriteLine("Штани застебнуто на блискавку");
    }
}

class Program
{
    static void Main()
    {
        Clothing c = new Clothing("Бавовна", "M");
        Shirt s = new Shirt("Льон", "L", 60);
        Pants p = new Pants("Джинс", "M", 80);

        Clothing[] items = { c, s, p };

        Console.WriteLine("Поліморфізм:");
        foreach (Clothing item in items)
        {
            item.Wear();
        }

        Console.WriteLine();
        Console.WriteLine("Власні методи:");
        s.ButtonUp();
        p.ZipUp();

        Console.WriteLine();
        Console.WriteLine("override і new:");
        Clothing cs = s;
        Console.Write("Wear() через Clothing: ");
        cs.Wear();
        Console.WriteLine("GetClothingType() через Shirt: " + s.GetClothingType());
        Console.WriteLine("GetClothingType() через Clothing: " + cs.GetClothingType());
    }
}