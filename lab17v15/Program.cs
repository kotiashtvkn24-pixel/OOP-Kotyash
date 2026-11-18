
#nullable enable

using System;
using System.IO;
using System.Net.Http;
using System.Threading;

class FileProcessor
{
    private int attempts = 0;

    public string LoadConfiguration(string path)
    {
        attempts++;

        Console.WriteLine("Спроба читання файлу: " + attempts);

        if (attempts <= 2)
            throw new IOException("Тимчасова помилка читання файлу");

        return "Конфігурацію успішно завантажено з " + path;
    }
}

class NetworkClient
{
    private int attempts = 0;

    public string DownloadConfiguration(string url)
    {
        attempts++;

        Console.WriteLine("Спроба завантаження конфігурації: " + attempts);

        if (attempts <= 3)
            throw new HttpRequestException("Тимчасова мережева помилка");

        return "Конфігурацію успішно завантажено з " + url;
    }
}

public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool>? shouldRetry = null)
    {
        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(200);

        for (int attempt = 0; ; attempt++)
        {
            try
            {
                Console.WriteLine("Виконання спроби " + (attempt + 1));

                T result = operation();

                Console.WriteLine("Операція успішна");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);

                bool retryAllowed = shouldRetry == null || shouldRetry(ex);

                if (!retryAllowed)
                {
                    Console.WriteLine("Повторення для цього винятку заборонено");
                    throw;
                }

                if (attempt >= retryCount)
                {
                    Console.WriteLine("Кількість повторних спроб вичерпана");
                    throw;
                }

                double delayMultiplier = Math.Pow(2, attempt);

                TimeSpan delay = TimeSpan.FromMilliseconds(
                    initialDelay.TotalMilliseconds * delayMultiplier);

                Console.WriteLine("Очікування: " + delay.TotalMilliseconds + " мс");

                Thread.Sleep(delay);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        FileProcessor fileProcessor = new FileProcessor();
        NetworkClient networkClient = new NetworkClient();

        Func<Exception, bool> shouldRetry = ex =>
            ex is IOException || ex is HttpRequestException;

        Console.WriteLine("=== LABORATORY WORK 17 ===");
        Console.WriteLine("Завантаження файлу конфігурації");

        Console.WriteLine("\n=== FILE PROCESSOR ===");

        try
        {
            string fileResult = RetryHelper.ExecuteWithRetry(
                () => fileProcessor.LoadConfiguration("config.json"),
                3,
                TimeSpan.FromMilliseconds(200),
                shouldRetry);

            Console.WriteLine(fileResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка обробки файлу: " + ex.Message);
        }

        Console.WriteLine("\n=== NETWORK CLIENT ===");

        try
        {
            string networkResult = RetryHelper.ExecuteWithRetry(
                () => networkClient.DownloadConfiguration("https://example.com/config"),
                3,
                TimeSpan.FromMilliseconds(200),
                shouldRetry);

            Console.WriteLine(networkResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка мережі: " + ex.Message);
        }

        Console.WriteLine("\n=== SHOULD RETRY CHECK ===");

        try
        {
            RetryHelper.ExecuteWithRetry<string>(
                () => throw new UnauthorizedAccessException("Доступ заборонено"),
                3,
                TimeSpan.FromMilliseconds(200),
                shouldRetry);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Оброблено виняток: " + ex.Message);
        }

        Console.WriteLine("\nКінець програми");
    }
}
