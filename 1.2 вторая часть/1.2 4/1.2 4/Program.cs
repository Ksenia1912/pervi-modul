using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размер массива K: ");
        int k = int.Parse(Console.ReadLine());

        Console.Write("Введите начало диапазона A: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите конец диапазона B: ");
        int b = int.Parse(Console.ReadLine());

        Random random = new Random();
        int[] array = new int[k];

        // Заполнение массива случайными числами
        for (int i = 0; i < k; i++)
        {
            array[i] = random.Next(a, b);
        }

        // Поиск индексов минимального и максимального элементов
        int minIndex = 0;
        int maxIndex = 0;

        for (int i = 1; i < k; i++)
        {
            if (array[i] < array[minIndex])
                minIndex = i;
            if (array[i] > array[maxIndex])
                maxIndex = i;
        }

        // Определение границ для вывода
        int start = Math.Min(minIndex, maxIndex);
        int end = Math.Max(minIndex, maxIndex);

        // Вывод результатов
        Console.WriteLine("Массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();

        Console.WriteLine($"Минимальный элемент: array[{minIndex}] = {array[minIndex]}");
        Console.WriteLine($"Максимальный элемент: array[{maxIndex}] = {array[maxIndex]}");

        Console.WriteLine("Элементы между минимальным и максимальным (включительно):");
        for (int i = start; i <= end; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }
}