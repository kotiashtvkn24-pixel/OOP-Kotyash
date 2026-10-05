using System;

class Logger
{
    public virtual void Log(string message)
    {
        Console.WriteLine("[Logger] " + message);
    }
}

class FileLogger : Logger
{
    public override void Log(string message)
    {
        Console.WriteLine("[FileLogger] Запис у файл: " + message);
    }
}

class ConsoleLogger : Logger
{
    public new void Log(string message)
    {
        Console.WriteLine("[ConsoleLogger] Вивід у консоль: " + message);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Logger obj0 = new Logger();
        Logger obj1 = new FileLogger();
        Logger obj2 = new ConsoleLogger();

        Console.WriteLine("--- Через посилання базового класу ---");
        obj0.Log("Logger");
        obj1.Log("FileLogger (override)");
        obj2.Log("ConsoleLogger (new)");

        Console.WriteLine();
        Console.WriteLine("--- Через посилання похідних класів ---");
        ((FileLogger)obj1).Log("FileLogger (override)");
        ((ConsoleLogger)obj2).Log("ConsoleLogger (new)");

        Console.WriteLine();
        Console.WriteLine("Пояснення:");
        Console.WriteLine("obj1 це Logger, що містить FileLogger. Метод override, тому виконується версія FileLogger.");
        Console.WriteLine("obj2 це Logger, що містить ConsoleLogger. Метод new, тому через Logger виконується версія базового класу.");
        Console.WriteLine("Версія ConsoleLogger викликається лише через приведення до ConsoleLogger.");
    }
}