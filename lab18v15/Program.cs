
using System;

public class MessageProcessingException : Exception
{
    public string MessageId { get; }
    public string Details { get; }

    public MessageProcessingException()
    {
        MessageId = "Unknown";
        Details = "Message processing error";
    }

    public MessageProcessingException(string message) : base(message)
    {
        MessageId = "Unknown";
        Details = message;
    }

    public MessageProcessingException(string message, Exception innerException)
        : base(message, innerException)
    {
        MessageId = "Unknown";
        Details = message;
    }

    public MessageProcessingException(string messageId, string details)
        : base(details)
    {
        MessageId = messageId;
        Details = details;
    }
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string Error { get; }

    private Result(T? value, bool isSuccess, string error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(default, false, error);
    }

    public static Result<T> Failure(Exception ex)
    {
        return new Result<T>(default, false, ex.Message);
    }
}

public class MessageProcessor
{
    public void ProcessMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message) || !message.Contains("|"))
        {
            string messageId = string.IsNullOrWhiteSpace(message)
                ? "Unknown"
                : message.Split('|')[0];

            throw new MessageProcessingException(
                messageId,
                "Повідомлення пошкоджене або має неправильний формат."
            );
        }

        string[] parts = message.Split('|', 2);

        if (string.IsNullOrWhiteSpace(parts[0]) ||
            string.IsNullOrWhiteSpace(parts[1]))
        {
            throw new MessageProcessingException(
                parts[0],
                "Відсутній ідентифікатор або текст повідомлення."
            );
        }

        Console.WriteLine($"Повідомлення {parts[0]} успішно оброблено.");
        Console.WriteLine($"Текст: {parts[1]}");
    }

    public Result<bool> SendMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message) || !message.Contains("|"))
        {
            return Result<bool>.Failure("Неправильний формат повідомлення.");
        }

        string[] parts = message.Split('|', 2);
        string recipient = parts[0];
        string content = parts[1];

        if (string.IsNullOrWhiteSpace(recipient) ||
            !recipient.Contains("@") ||
            !recipient.Contains("."))
        {
            return Result<bool>.Failure("Некоректна адреса одержувача.");
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            return Result<bool>.Failure("Текст повідомлення не може бути порожнім.");
        }

        return Result<bool>.Success(true);
    }
}

public class Program
{
    public static void Main()
    {
        MessageProcessor processor = new MessageProcessor();

        Console.WriteLine("=== Обробка повідомлень ===");

        try
        {
            processor.ProcessMessage("MSG001|Привіт! Це тестове повідомлення.");
            processor.ProcessMessage("MSG002");
        }
        catch (MessageProcessingException ex)
        {
            Console.WriteLine("Власний виняток:");
            Console.WriteLine($"ID повідомлення: {ex.MessageId}");
            Console.WriteLine($"Помилка: {ex.Details}");
        }

        Console.WriteLine();
        Console.WriteLine("=== Відправлення повідомлень ===");

        Result<bool> success = processor.SendMessage(
            "user@example.com|Привіт! Як справи?"
        );

        if (success.IsSuccess)
        {
            Console.WriteLine("Повідомлення успішно відправлено.");
        }
        else
        {
            Console.WriteLine($"Помилка: {success.Error}");
        }

        Result<bool> failure = processor.SendMessage(
            "invalid-email|Тестове повідомлення"
        );

        if (failure.IsSuccess)
        {
            Console.WriteLine("Повідомлення успішно відправлено.");
        }
        else
        {
            Console.WriteLine($"Помилка: {failure.Error}");
        }
    }
}
