using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());

        double[] array = new double[n];

        // Ввод элементов массива
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Введите элемент [{i}]: ");
            array[i] = double.Parse(Console.ReadLine());
        }

        // Поиск максимального по модулю элемента
        double maxAbs = Math.Abs(array[0]);
        for (int i = 1; i < n; i++)
        {
            if (Math.Abs(array[i]) > maxAbs)
                maxAbs = Math.Abs(array[i]);
        }

        // Нормировка массива
        for (int i = 0; i < n; i++)
        {
            array[i] /= maxAbs;
        }

        // Вывод результата
        Console.WriteLine("Нормированный массив:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"array[{i}] = {array[i]:F4}");
        }
    }
}