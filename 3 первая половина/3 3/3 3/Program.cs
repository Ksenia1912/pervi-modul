using System;
using System.Collections.Generic;

// Делегат для выполнения задачи
public delegate void TaskDelegate(string taskDescription);

// Класс Задача
class Task
{
    public string Description { get; set; }
    public TaskDelegate TaskHandler { get; set; }
    public DateTime CreatedAt { get; set; }

    public Task(string description, TaskDelegate handler)
    {
        Description = description;
        TaskHandler = handler;
        CreatedAt = DateTime.Now;
    }

    public void Execute()
    {
        TaskHandler?.Invoke(Description);
    }
}

// Класс Менеджер задач
class TaskManager
{
    private List<Task> tasks = new List<Task>();

    // Методы-обработчики задач
    public static void SendNotification(string taskDescription)
    {
        Console.WriteLine($"[УВЕДОМЛЕНИЕ] Отправка уведомления для задачи: {taskDescription}");
        Console.WriteLine("    Статус: Уведомление отправлено успешно\n");
    }

    public static void LogToJournal(string taskDescription)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {taskDescription}";
        Console.WriteLine($"[ЖУРНАЛ] Запись в журнал: {taskDescription}");
        Console.WriteLine($"    Запись: {logEntry}\n");
    }

    public static void SendEmail(string taskDescription)
    {
        Console.WriteLine($"[EMAIL] Отправка email для задачи: {taskDescription}");
        Console.WriteLine("    Статус: Email отправлен на почту\n");
    }

    public static void BackupData(string taskDescription)
    {
        Console.WriteLine($"[БЭКАП] Резервное копирование для задачи: {taskDescription}");
        Console.WriteLine("    Статус: Данные сохранены в backup\n");
    }

    // Добавление задачи
    public void AddTask(string description, TaskDelegate handler)
    {
        Task task = new Task(description, handler);
        tasks.Add(task);
        Console.WriteLine($"Задача добавлена: {description}");
    }

    // Выполнение всех задач
    public void ExecuteAllTasks()
    {
        Console.WriteLine("\nВыполнение всех задач:");

        foreach (var task in tasks)
        {
            Console.WriteLine($"Задача: {task.Description}");
            task.Execute();
        }

        tasks.Clear();
        Console.WriteLine("Все задачи выполнены и очищены из списка.");
    }

    // Выполнение задач по фильтру
    public void ExecuteTasksByFilter(Predicate<Task> filter)
    {
        Console.WriteLine("\nВыполнение отфильтрованных задач:");

        var filteredTasks = tasks.FindAll(filter);
        foreach (var task in filteredTasks)
        {
            task.Execute();
        }
    }
}

class Program
{
    static void Main()
    {
        TaskManager taskManager = new TaskManager();

        Console.WriteLine("Система управления задачами с делегатами:");

        // Создание делегатов
        TaskDelegate notifyDelegate = TaskManager.SendNotification;
        TaskDelegate logDelegate = TaskManager.LogToJournal;
        TaskDelegate emailDelegate = TaskManager.SendEmail;
        TaskDelegate backupDelegate = TaskManager.BackupData;

        // Комбинированный делегат
        TaskDelegate multiTaskDelegate = TaskManager.SendNotification;
        multiTaskDelegate += TaskManager.LogToJournal;
        multiTaskDelegate += TaskManager.SendEmail;

        // Добавление задач
        taskManager.AddTask("Проверить систему безопасности", notifyDelegate);
        taskManager.AddTask("Обновить базу данных", logDelegate);
        taskManager.AddTask("Отправить отчет руководству", emailDelegate);
        taskManager.AddTask("Создать резервную копию", backupDelegate);
        taskManager.AddTask("Комплексная задача: аудит системы", multiTaskDelegate);

        // Выполнение всех задач
        taskManager.ExecuteAllTasks();

        // Добавление новых задач для демонстрации фильтрации
        Console.WriteLine("\nДобавление новых задач для демонстрации фильтрации:");
        taskManager.AddTask("Срочное уведомление", notifyDelegate);
        taskManager.AddTask("Логирование ошибок", logDelegate);
        taskManager.AddTask("Рассылка новостей", emailDelegate);
        taskManager.AddTask("Резервное копирование логов", backupDelegate);

        // Фильтрация задач по ключевым словам
        taskManager.ExecuteTasksByFilter(task => task.Description.ToLower().Contains("уведомление"));
        taskManager.ExecuteTasksByFilter(task => task.Description.ToLower().Contains("резерв"));
    }
}