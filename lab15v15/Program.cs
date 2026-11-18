
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int SupplierId { get; set; }
}

class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
}

class Program
{
    static T Measure<T>(string name, Func<T> action)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        T result = action();
        stopwatch.Stop();

        Console.WriteLine(name + ": " + stopwatch.Elapsed.TotalMilliseconds.ToString("F4") + " мс");

        return result;
    }

    static void Main()
    {
        List<Supplier> suppliers = new List<Supplier>
        {
            new Supplier { Id = 1, Name = "Global Supplies", Country = "USA" },
            new Supplier { Id = 2, Name = "Tech World", Country = "Germany" },
            new Supplier { Id = 3, Name = "Best Products", Country = "Poland" },
            new Supplier { Id = 4, Name = "Smart Trade", Country = "Ukraine" },
            new Supplier { Id = 5, Name = "Mega Store", Country = "China" }
        };

        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Mouse", Price = 25, SupplierId = 1 },
            new Product { Id = 2, Name = "Keyboard", Price = 45, SupplierId = 1 },
            new Product { Id = 3, Name = "USB Cable", Price = 10, SupplierId = 1 },
            new Product { Id = 4, Name = "Headphones", Price = 85, SupplierId = 1 },
            new Product { Id = 5, Name = "Monitor", Price = 250, SupplierId = 2 },
            new Product { Id = 6, Name = "Laptop", Price = 850, SupplierId = 2 },
            new Product { Id = 7, Name = "Webcam", Price = 70, SupplierId = 2 },
            new Product { Id = 8, Name = "Phone", Price = 600, SupplierId = 3 },
            new Product { Id = 9, Name = "Charger", Price = 15, SupplierId = 3 },
            new Product { Id = 10, Name = "Tablet", Price = 300, SupplierId = 3 },
            new Product { Id = 11, Name = "Printer", Price = 180, SupplierId = 4 },
            new Product { Id = 12, Name = "Flash Drive", Price = 12, SupplierId = 4 },
            new Product { Id = 13, Name = "Camera", Price = 450, SupplierId = 4 },
            new Product { Id = 14, Name = "Speaker", Price = 55, SupplierId = 5 },
            new Product { Id = 15, Name = "Microphone", Price = 95, SupplierId = 5 }
        };

        Console.WriteLine("=== LINQ: УПРАВЛІННЯ ТОВАРАМИ НА СКЛАДІ ===");

        Console.WriteLine("\n=== 1. ФІЛЬТРАЦІЯ ===");

        var cheapProductsQuery = Measure("Синтаксис запитів", () =>
            (from p in products
             join s in suppliers on p.SupplierId equals s.Id
             where s.Name == "Global Supplies" && p.Price < 100
             select p).ToList());

        foreach (var product in cheapProductsQuery)
            Console.WriteLine(product.Name + " - " + product.Price);

        var cheapProductsMethod = Measure("Синтаксис методів", () =>
            products.Join(suppliers,
                p => p.SupplierId,
                s => s.Id,
                (p, s) => new { Product = p, Supplier = s })
            .Where(x => x.Supplier.Name == "Global Supplies" && x.Product.Price < 100)
            .Select(x => x.Product)
            .ToList());

        foreach (var product in cheapProductsMethod)
            Console.WriteLine(product.Name + " - " + product.Price);

        Console.WriteLine("\n=== 2. JOIN ===");

        var joinQuery = Measure("Синтаксис запитів", () =>
            (from p in products
             join s in suppliers on p.SupplierId equals s.Id
             select new { Product = p.Name, Supplier = s.Name }).ToList());

        foreach (var item in joinQuery)
            Console.WriteLine(item.Product + " - " + item.Supplier);

        var joinMethod = Measure("Синтаксис методів", () =>
            products.Join(suppliers,
                p => p.SupplierId,
                s => s.Id,
                (p, s) => new { Product = p.Name, Supplier = s.Name }).ToList());

        foreach (var item in joinMethod)
            Console.WriteLine(item.Product + " - " + item.Supplier);

        Console.WriteLine("\n=== 3. КІЛЬКІСТЬ ТОВАРІВ ПОСТАЧАЛЬНИКІВ ===");

        var countQuery = Measure("Синтаксис запитів", () =>
            (from p in products
             join s in suppliers on p.SupplierId equals s.Id
             group p by s.Name into g
             select new { Supplier = g.Key, Count = g.Count() }).ToList());

        foreach (var item in countQuery)
            Console.WriteLine(item.Supplier + ": " + item.Count);

        var countMethod = Measure("Синтаксис методів", () =>
            suppliers.GroupJoin(products,
                s => s.Id,
                p => p.SupplierId,
                (s, productList) => new
                {
                    Supplier = s.Name,
                    Count = productList.Count()
                }).ToList());

        foreach (var item in countMethod)
            Console.WriteLine(item.Supplier + ": " + item.Count);

        Console.WriteLine("\n=== 4. НАЙДЕШЕВШИЙ ТОВАР ===");

        var cheapestQuery = Measure("Синтаксис запитів", () =>
            (from p in products
             orderby p.Price
             select p).FirstOrDefault());

        if (cheapestQuery != null)
            Console.WriteLine(cheapestQuery.Name + " - " + cheapestQuery.Price);

        var cheapestMethod = Measure("Синтаксис методів", () =>
            products.OrderBy(p => p.Price).FirstOrDefault());

        if (cheapestMethod != null)
            Console.WriteLine(cheapestMethod.Name + " - " + cheapestMethod.Price);

        Console.WriteLine("\n=== ВИСНОВОК ===");
        Console.WriteLine("У роботі використано Where, Join, GroupBy, Count, OrderBy та FirstOrDefault.");
        Console.WriteLine("LINQ дозволяє зручно фільтрувати, об'єднувати та групувати дані.");
        Console.WriteLine("Час виконання залежить від кількості даних та складності запиту.");
        Console.WriteLine("На невеликій кількості даних різниця в часі може бути незначною.");

        Console.WriteLine("\nКінець програми");
    }
}
