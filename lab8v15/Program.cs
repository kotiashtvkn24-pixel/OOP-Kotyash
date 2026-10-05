using System;
using System.Collections.Generic;
using System.Linq;

class FinancialInstrument
{
    public string Name { get; set; }

    public FinancialInstrument(string name)
    {
        Name = name;
    }

    public virtual decimal GetCurrentValue()
    {
        return 0;
    }
}

class Stock : FinancialInstrument
{
    public string TickerSymbol { get; set; }
    public decimal SharePrice { get; set; }

    public Stock(string name, string tickerSymbol, decimal sharePrice) : base(name)
    {
        TickerSymbol = tickerSymbol;
        SharePrice = sharePrice;
    }

    public override decimal GetCurrentValue()
    {
        return SharePrice;
    }
}

class Bond : FinancialInstrument
{
    public decimal FaceValue { get; set; }
    public decimal InterestRate { get; set; }

    public Bond(string name, decimal faceValue, decimal interestRate) : base(name)
    {
        FaceValue = faceValue;
        InterestRate = interestRate;
    }

    public override decimal GetCurrentValue()
    {
        return FaceValue + FaceValue * InterestRate / 100;
    }
}

class MutualFund : FinancialInstrument
{
    public decimal NAV { get; set; }
    public int NumShares { get; set; }

    public MutualFund(string name, decimal nav, int numShares) : base(name)
    {
        NAV = nav;
        NumShares = numShares;
    }

    public override decimal GetCurrentValue()
    {
        return NAV * NumShares;
    }
}

class Program
{
    static void Main()
    {
        List<FinancialInstrument> items = new List<FinancialInstrument>();
        items.Add(new Stock("Apple", "AAPL", 150m));
        items.Add(new Bond("Government Bond", 1000m, 5m));
        items.Add(new MutualFund("Growth Fund", 25m, 40));

        Console.WriteLine("Інструменти:");
        foreach (FinancialInstrument item in items)
        {
            Console.WriteLine($"{item.Name} ({item.GetType().Name}): {item.GetCurrentValue():F0}");
        }

        decimal total = items.Sum(i => i.GetCurrentValue());
        decimal max = items.Max(i => i.GetCurrentValue());
        decimal min = items.Min(i => i.GetCurrentValue());

        Console.WriteLine();
        Console.WriteLine($"Загальна вартість: {total:F0}");
        Console.WriteLine($"Найбільша вартість: {max:F0}");
        Console.WriteLine($"Найменша вартість: {min:F0}");
    }
}