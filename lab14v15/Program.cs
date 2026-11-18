
using System;
using System.Collections.Generic;

class NotificationSystem
{
    private HashSet<string> users = new HashSet<string>();
    private Queue<string> notifications = new Queue<string>();
    private Stack<string> readNotifications = new Stack<string>();

    public void AddUser(string name)
    {
        if (users.Add(name))
            Console.WriteLine("Користувача додано: " + name);
        else
            Console.WriteLine("Користувач вже існує: " + name);
    }

    public void ShowUsers()
    {
        Console.WriteLine("\nКористувачі зі сповіщеннями:");

        foreach (string user in users)
            Console.WriteLine(user);
    }

    public void AddNotification(string message)
    {
        notifications.Enqueue(message);
        Console.WriteLine("Додано сповіщення: " + message);
    }

    public void ReadNotification()
    {
        if (notifications.Count > 0)
        {
            string message = notifications.Dequeue();
            readNotifications.Push(message);
            Console.WriteLine("Прочитано: " + message);
        }
        else
        {
            Console.WriteLine("Немає нових сповіщень");
        }
    }

    public void ShowNextNotification()
    {
        if (notifications.Count > 0)
            Console.WriteLine("Наступне сповіщення: " + notifications.Peek());
        else
            Console.WriteLine("Черга порожня");
    }

    public void ShowReadHistory()
    {
        Console.WriteLine("\nІсторія прочитаних сповіщень:");

        foreach (string message in readNotifications)
            Console.WriteLine(message);
    }

    public void UndoRead()
    {
        if (readNotifications.Count > 0)
            Console.WriteLine("Останнє прочитане: " + readNotifications.Pop());
        else
            Console.WriteLine("Історія порожня");
    }
}

class Program
{
    static void Main()
    {
        NotificationSystem system = new NotificationSystem();

        Console.WriteLine("=== HASHSET ===");
        system.AddUser("Taras");
        system.AddUser("Ivan");
        system.AddUser("Olena");
        system.AddUser("Taras");
        system.ShowUsers();

        Console.WriteLine("\n=== QUEUE ===");
        system.AddNotification("Нове повідомлення");
        system.AddNotification("Оновлення програми");
        system.AddNotification("Нове замовлення");

        system.ShowNextNotification();
        system.ReadNotification();
        system.ReadNotification();

        Console.WriteLine("\n=== STACK ===");
        system.ShowReadHistory();
        system.UndoRead();
        system.ShowReadHistory();

        Console.WriteLine("\nКінець програми");
    }
}
