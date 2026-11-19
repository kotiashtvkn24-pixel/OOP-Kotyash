
using System;
using System.Collections.Generic;
using System.Linq;

class Specification
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}

class Component
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Specification> Specifications { get; set; } = new List<Specification>();
}

static class ComponentExtensions
{
    public static string GetSpecification(this Component component, string key)
    {
        return component.Specifications.FirstOrDefault(s =>
            s.Key.Equals(key, StringComparison.OrdinalIgnoreCase))?.Value ?? "Не знайдено";
    }
}

class Computer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Component> Components { get; set; } = new List<Component>();
}

class Program
{
    static void Main()
    {
        List<Computer> computers = new List<Computer>
        {
            new Computer
            {
                Id = 1,
                Name = "Gaming PC",
                Components = new List<Component>
                {
                    new Component { Id = 1, Name = "CPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 1, Key = "Model", Value = "Intel Core i7" },
                        new Specification { Id = 2, Key = "Cores", Value = "8" },
                        new Specification { Id = 3, Key = "Frequency", Value = "5.0 GHz" }
                    }},
                    new Component { Id = 2, Name = "GPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 4, Key = "Model", Value = "RTX 4070" },
                        new Specification { Id = 5, Key = "Memory", Value = "12 GB" }
                    }},
                    new Component { Id = 3, Name = "RAM", Specifications = new List<Specification>
                    {
                        new Specification { Id = 6, Key = "Capacity", Value = "32 GB" },
                        new Specification { Id = 7, Key = "Type", Value = "DDR5" }
                    }}
                }
            },
            new Computer
            {
                Id = 2,
                Name = "Office PC",
                Components = new List<Component>
                {
                    new Component { Id = 4, Name = "CPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 8, Key = "Model", Value = "Intel Core i5" },
                        new Specification { Id = 9, Key = "Cores", Value = "6" }
                    }},
                    new Component { Id = 5, Name = "GPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 10, Key = "Model", Value = "Intel UHD" },
                        new Specification { Id = 11, Key = "Memory", Value = "Shared" }
                    }},
                    new Component { Id = 6, Name = "SSD", Specifications = new List<Specification>
                    {
                        new Specification { Id = 12, Key = "Capacity", Value = "512 GB" },
                        new Specification { Id = 13, Key = "Type", Value = "NVMe" }
                    }}
                }
            },
            new Computer
            {
                Id = 3,
                Name = "Workstation",
                Components = new List<Component>
                {
                    new Component { Id = 7, Name = "CPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 14, Key = "Model", Value = "AMD Ryzen 9" },
                        new Specification { Id = 15, Key = "Cores", Value = "16" }
                    }},
                    new Component { Id = 8, Name = "GPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 16, Key = "Model", Value = "RTX 4080" },
                        new Specification { Id = 17, Key = "Memory", Value = "16 GB" }
                    }},
                    new Component { Id = 9, Name = "RAM", Specifications = new List<Specification>
                    {
                        new Specification { Id = 18, Key = "Capacity", Value = "64 GB" },
                        new Specification { Id = 19, Key = "Type", Value = "DDR5" }
                    }}
                }
            },
            new Computer
            {
                Id = 4,
                Name = "Budget PC",
                Components = new List<Component>
                {
                    new Component { Id = 10, Name = "CPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 20, Key = "Model", Value = "AMD Ryzen 5" },
                        new Specification { Id = 21, Key = "Cores", Value = "6" }
                    }},
                    new Component { Id = 11, Name = "GPU", Specifications = new List<Specification>
                    {
                        new Specification { Id = 22, Key = "Model", Value = "GTX 1660" },
                        new Specification { Id = 23, Key = "Memory", Value = "6 GB" }
                    }},
                    new Component { Id = 12, Name = "SSD", Specifications = new List<Specification>
                    {
                        new Specification { Id = 24, Key = "Capacity", Value = "256 GB" },
                        new Specification { Id = 25, Key = "Type", Value = "SATA" }
                    }}
                }
            }
        };

        Console.WriteLine("=== LABORATORY WORK 16 ===");
        Console.WriteLine("Computers and their components");

        Console.WriteLine("\n=== 1. SELECT MANY ===");

        var allSpecifications = computers
            .SelectMany(c => c.Components)
            .SelectMany(component => component.Specifications)
            .ToList();

        foreach (var specification in allSpecifications)
        {
            Console.WriteLine(specification.Key + ": " + specification.Value);
        }

        Console.WriteLine("\nTotal specifications: " + allSpecifications.Count);

        Console.WriteLine("\n=== 2. PAGINATION ===");

        var allComponents = computers.SelectMany(c => c.Components).ToList();
        int pageSize = 4;
        int totalPages = (int)Math.Ceiling(allComponents.Count / (double)pageSize);

        for (int page = 1; page <= totalPages; page++)
        {
            Console.WriteLine("\nPage " + page + ":");

            var pageComponents = allComponents
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            foreach (var component in pageComponents)
            {
                Console.WriteLine(component.Name);
            }
        }

        Console.WriteLine("\n=== 3. CUSTOM EXTENSION METHOD ===");

        foreach (var computer in computers)
        {
            Console.WriteLine("\nComputer: " + computer.Name);

            foreach (var component in computer.Components)
            {
                Console.WriteLine(component.Name + " - Model: " +
                    component.GetSpecification("Model"));
            }
        }

        Console.WriteLine("\n=== 4. SEARCH SPECIFICATION ===");

        Component selectedComponent = allComponents.First();

        Console.WriteLine("Component: " + selectedComponent.Name);
        Console.WriteLine("Model: " + selectedComponent.GetSpecification("Model"));
        Console.WriteLine("Memory: " + selectedComponent.GetSpecification("Memory"));
        Console.WriteLine("Cores: " + selectedComponent.GetSpecification("Cores"));

        Console.WriteLine("\nProgram finished.");
    }
}
