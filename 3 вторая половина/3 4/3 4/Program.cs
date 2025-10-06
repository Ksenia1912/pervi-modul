using System;
using System.Collections.Generic;
using System.Linq;

// Делегат для фильтрации
public delegate bool FilterDelegate<T>(T item);

// Класс для работы с данными
class DataManager<T>
{
    private List<T> data;

    public DataManager(List<T> initialData)
    {
        data = initialData;
    }

    // Метод фильтрации с использованием делегата
    public List<T> FilterData(FilterDelegate<T> filter)
    {
        return data.Where(item => filter(item)).ToList();
    }

    // Вывод данных
    public void DisplayData(string title, List<T> dataToDisplay = null)
    {
        var displayData = dataToDisplay ?? data;

        Console.WriteLine(title);

        foreach (var item in displayData)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
    }
}

// Класс для представления записи
class DataRecord
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string[] Keywords { get; set; }
    public int Priority { get; set; }

    public DataRecord(string title, DateTime date, string[] keywords, int priority)
    {
        Title = title;
        Date = date;
        Keywords = keywords;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd} | Приоритет: {Priority} | {Title} [Ключевые слова: {string.Join(", ", Keywords)}]";
    }
}

class Program
{
    static void Main()
    {
        // Создание тестовых данных
        var records = new List<DataRecord>
        {
            new DataRecord("Отчет за январь", new DateTime(2024, 1, 15), new[] { "отчет", "финансы" }, 2),
            new DataRecord("Важная встреча", new DateTime(2024, 2, 1), new[] { "встреча", "срочно" }, 1),
            new DataRecord("Презентация проекта", new DateTime(2024, 1, 20), new[] { "презентация", "проект" }, 3),
            new DataRecord("Бюджет на квартал", new DateTime(2024, 2, 10), new[] { "бюджет", "финансы" }, 1),
            new DataRecord("Обучение сотрудников", new DateTime(2024, 1, 25), new[] { "обучение", "кадры" }, 2),
            new DataRecord("Срочный ремонт", new DateTime(2024, 2, 5), new[] { "ремонт", "срочно" }, 1)
        };

        DataManager<DataRecord> dataManager = new DataManager<DataRecord>(records);

        Console.WriteLine("Система фильтрации данных с делегатами:");

        // Отображение всех данных
        dataManager.DisplayData("ВСЕ ДАННЫХ:");

        // Различные фильтры с использованием делегатов

        // Фильтр по дате (за последний месяц)
        FilterDelegate<DataRecord> dateFilter = record => record.Date >= new DateTime(2024, 2, 1);
        var filteredByDate = dataManager.FilterData(dateFilter);
        dataManager.DisplayData("ДАННЫЕ ЗА ФЕВРАЛЬ 2024:", filteredByDate);

        // Фильтр по ключевым словам
        FilterDelegate<DataRecord> keywordFilter = record => record.Keywords.Any(kw => kw == "срочно");
        var filteredByKeywords = dataManager.FilterData(keywordFilter);
        dataManager.DisplayData("СРОЧНЫЕ ЗАДАЧИ:", filteredByKeywords);

        // Фильтр по приоритету
        FilterDelegate<DataRecord> priorityFilter = record => record.Priority == 1;
        var highPriority = dataManager.FilterData(priorityFilter);
        dataManager.DisplayData("ВЫСОКИЙ ПРИОРИТЕТ (1):", highPriority);

        // Комбинированный фильтр с использованием лямбда-выражения
        var complexFilter = dataManager.FilterData(record =>
            record.Date.Month == 1 && record.Keywords.Any(kw => kw == "финансы"));
        dataManager.DisplayData("ФИНАНСОВЫЕ ОТЧЕТЫ ЗА ЯНВАРЬ:", complexFilter);

        // Динамическое создание фильтров
        Console.WriteLine("ДИНАМИЧЕСКАЯ ФИЛЬТРАЦИЯ:");

        // Пользовательский ввод фильтра (имитация)
        string searchKeyword = "проект";
        FilterDelegate<DataRecord> dynamicFilter = record =>
            record.Title.ToLower().Contains(searchKeyword.ToLower()) ||
            record.Keywords.Any(kw => kw.ToLower().Contains(searchKeyword.ToLower()));

        var dynamicResults = dataManager.FilterData(dynamicFilter);
        dataManager.DisplayData($"РЕЗУЛЬТАТЫ ПОИСКА ПО '{searchKeyword}':", dynamicResults);

        // Многоуровневая фильтрация
        Console.WriteLine("МНОГОУРОВНЕВАЯ ФИЛЬТРАЦИЯ:");

        // Цепочка фильтров
        var multiFiltered = records
            .Where(record => record.Priority <= 2)
            .Where(record => record.Date >= new DateTime(2024, 1, 20))
            .ToList();

        dataManager.DisplayData("ЗАДАЧИ ПРИОРИТЕТА 1-2 ПОСЛЕ 20 ЯНВАРЯ:", multiFiltered);
    }
}