using System;
using System.Collections.Generic;

// Делегат для сортировки
public delegate void SortDelegate(List<int> data);

// Класс для управления сортировкой
class SortManager
{
    // Метод пузырьковой сортировки
    public static void BubbleSort(List<int> data)
    {
        int n = data.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (data[j] > data[j + 1])
                {
                    // Обмен элементов
                    int temp = data[j];
                    data[j] = data[j + 1];
                    data[j + 1] = temp;
                }
            }
        }
    }

    // Метод быстрой сортировки
    public static void QuickSort(List<int> data)
    {
        if (data.Count <= 1) return;
        QuickSortRecursive(data, 0, data.Count - 1);
    }

    private static void QuickSortRecursive(List<int> data, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(data, left, right);
            QuickSortRecursive(data, left, pivotIndex - 1);
            QuickSortRecursive(data, pivotIndex + 1, right);
        }
    }

    private static int Partition(List<int> data, int left, int right)
    {
        int pivot = data[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (data[j] <= pivot)
            {
                i++;
                Swap(data, i, j);
            }
        }

        Swap(data, i + 1, right);
        return i + 1;
    }

    private static void Swap(List<int> data, int i, int j)
    {
        int temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }

    // Метод сортировки выбором
    public static void SelectionSort(List<int> data)
    {
        int n = data.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (data[j] < data[minIndex])
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                Swap(data, i, minIndex);
            }
        }
    }

    // Метод сортировки вставками
    public static void InsertionSort(List<int> data)
    {
        for (int i = 1; i < data.Count; i++)
        {
            int key = data[i];
            int j = i - 1;

            while (j >= 0 && data[j] > key)
            {
                data[j + 1] = data[j];
                j--;
            }
            data[j + 1] = key;
        }
    }

    // Сортировка по убыванию
    public static void SortDescending(List<int> data)
    {
        data.Sort((a, b) => b.CompareTo(a));
    }

    // Генерация случайных данных
    public static List<int> GenerateRandomData(int count, int min = 1, int max = 1000)
    {
        Random random = new Random();
        List<int> data = new List<int>();

        for (int i = 0; i < count; i++)
        {
            data.Add(random.Next(min, max));
        }

        return data;
    }

    // Вывод данных
    public static void DisplayData(List<int> data, string title, int maxDisplay = 20)
    {
        Console.WriteLine(title);
        Console.Write("[");

        int displayCount = Math.Min(data.Count, maxDisplay);
        for (int i = 0; i < displayCount; i++)
        {
            Console.Write(data[i]);
            if (i < displayCount - 1) Console.Write(", ");
        }

        if (data.Count > maxDisplay)
        {
            Console.Write(", ...");
        }
        Console.WriteLine($"] (всего элементов: {data.Count})");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Приложение для сортировки числовых данных");

        // Создание делегатов для разных методов сортировки
        var sortMethods = new Dictionary<string, SortDelegate>
        {
            { "1", SortManager.BubbleSort },
            { "2", SortManager.QuickSort },
            { "3", SortManager.SelectionSort },
            { "4", SortManager.InsertionSort },
            { "5", SortManager.SortDescending }
        };

        bool continueRunning = true;

        while (continueRunning)
        {
            Console.Clear();
            DisplayMenu();

            string choice = Console.ReadLine();

            if (choice == "6")
            {
                continueRunning = false;
                Console.WriteLine("Выход из программы...");
                continue;
            }

            if (sortMethods.ContainsKey(choice))
            {
                ProcessUserChoice(choice, sortMethods);
            }
            else
            {
                Console.WriteLine("Неверный выбор! Пожалуйста, выберите от 1 до 6.");
                WaitForUser();
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("ВЫБЕРИТЕ МЕТОД СОРТИРОВКИ:");
        Console.WriteLine("1. Пузырьковая сортировка");
        Console.WriteLine("2. Быстрая сортировка");
        Console.WriteLine("3. Сортировка выбором");
        Console.WriteLine("4. Сортировка вставками");
        Console.WriteLine("5. Сортировка по убыванию");
        Console.WriteLine("6. Выход");
        Console.Write("\nВаш выбор: ");
    }

    static void ProcessUserChoice(string choice, Dictionary<string, SortDelegate> sortMethods)
    {
        // Выбор размера данных
        Console.Write("\nВведите количество элементов для сортировки (по умолчанию 100): ");
        string sizeInput = Console.ReadLine();
        int size = string.IsNullOrEmpty(sizeInput) ? 100 : int.Parse(sizeInput);

        // Генерация данных
        List<int> data = SortManager.GenerateRandomData(size);

        Console.WriteLine($"\nСгенерировано {size} случайных чисел.");
        SortManager.DisplayData(data, "Исходные данные:");

        // Выполнение сортировки
        string methodName = GetMethodName(choice);
        Console.WriteLine($"\nВыполняется {methodName}...");

        // Создаем копию для сортировки
        List<int> dataToSort = new List<int>(data);
        sortMethods[choice](dataToSort);

        SortManager.DisplayData(dataToSort, $"После {methodName}:");

        WaitForUser();
    }

    static string GetMethodName(string choice)
    {
        return choice switch
        {
            "1" => "Пузырьковой сортировки",
            "2" => "Быстрой сортировки",
            "3" => "Сортировки выбором",
            "4" => "Сортировки вставками",
            "5" => "Сортировки по убыванию",
            _ => "Неизвестный метод"
        };
    }

    static void WaitForUser()
    {
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}