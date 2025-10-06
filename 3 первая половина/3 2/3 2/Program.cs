using System;

// Класс для аргументов события уведомления
public class NotificationEventArgs : EventArgs
{
    public string Message { get; }
    public string Sender { get; }
    public DateTime Timestamp { get; }

    public NotificationEventArgs(string message, string sender)
    {
        Message = message;
        Sender = sender;
        Timestamp = DateTime.Now;
    }
}

// Класс Уведомление
class NotificationManager
{
    // События для разных типов уведомлений
    public event EventHandler<NotificationEventArgs> MessageReceived;
    public event EventHandler<NotificationEventArgs> CallReceived;
    public event EventHandler<NotificationEventArgs> EmailReceived;

    // Методы для генерации событий
    public void SendMessage(string message, string sender)
    {
        Console.WriteLine($"Отправка сообщения от {sender}: {message}");
        MessageReceived?.Invoke(this, new NotificationEventArgs(message, sender));
    }

    public void MakeCall(string message, string caller)
    {
        Console.WriteLine($"Входящий звонок от {caller}: {message}");
        CallReceived?.Invoke(this, new NotificationEventArgs(message, caller));
    }

    public void SendEmail(string message, string sender)
    {
        Console.WriteLine($"Отправка email от {sender}: {message}");
        EmailReceived?.Invoke(this, new NotificationEventArgs(message, sender));
    }
}

// Класс-обработчик уведомлений
class NotificationHandler
{
    private string handlerName;

    public NotificationHandler(string name)
    {
        handlerName = name;
    }

    // Обработчики событий
    public void OnMessageReceived(object sender, NotificationEventArgs e)
    {
        Console.WriteLine($"[{handlerName}] СООБЩЕНИЕ получено в {e.Timestamp:HH:mm:ss}");
        Console.WriteLine($"    От: {e.Sender}");
        Console.WriteLine($"    Текст: {e.Message}");
        Console.WriteLine("    Действие: Показать всплывающее уведомление\n");
    }

    public void OnCallReceived(object sender, NotificationEventArgs e)
    {
        Console.WriteLine($"[{handlerName}] ЗВОНОК получен в {e.Timestamp:HH:mm:ss}");
        Console.WriteLine($"    От: {e.Sender}");
        Console.WriteLine($"    Тема: {e.Message}");
        Console.WriteLine("    Действие: Воспроизвести рингтон\n");
    }

    public void OnEmailReceived(object sender, NotificationEventArgs e)
    {
        Console.WriteLine($"[{handlerName}] EMAIL получен в {e.Timestamp:HH:mm:ss}");
        Console.WriteLine($"    От: {e.Sender}");
        Console.WriteLine($"    Тема: {e.Message}");
        Console.WriteLine("    Действие: Добавить в папку 'Входящие'\n");
    }
}

class Program
{
    static void Main()
    {
        // Создание менеджера уведомлений
        NotificationManager notificationManager = new NotificationManager();

        // Создание обработчиков
        NotificationHandler mainHandler = new NotificationHandler("Основной обработчик");
        NotificationHandler backupHandler = new NotificationHandler("Резервный обработчик");

        // Подписка на события
        notificationManager.MessageReceived += mainHandler.OnMessageReceived;
        notificationManager.CallReceived += mainHandler.OnCallReceived;
        notificationManager.EmailReceived += mainHandler.OnEmailReceived;

        // Дополнительная подписка для демонстрации
        notificationManager.MessageReceived += backupHandler.OnMessageReceived;

        Console.WriteLine("Система уведомлений мобильного приложения:");

        // Генерация уведомлений
        notificationManager.SendMessage("Привет! Как дела?", "Анна");
        notificationManager.MakeCall("Обсудить проект", "Иван Петров");
        notificationManager.SendEmail("Отчет за месяц", "бухгалтерия@company.com");
        notificationManager.SendMessage("Напоминание: встреча в 15:00", "Календарь");

        // Отписка от события
        notificationManager.MessageReceived -= backupHandler.OnMessageReceived;

        Console.WriteLine("После отписки резервного обработчика:");

        notificationManager.SendMessage("Это сообщение увидят только основные обработчики", "Система");
    }
}