using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        int[] numbers = new int[20];

        // Заполнение массива случайными числами
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(1, 101); // числа от 1 до 100
        }

        // Поиск минимального и максимального значения
        int min = numbers[0];
        int max = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] < min)
                min = numbers[i];
            if (numbers[i] > max)
                max = numbers[i];
        }

        // Вывод результатов
        Console.WriteLine("Массив чисел:");
        foreach (int number in numbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine($"\nМинимальное значение: {min}");
        Console.WriteLine($"Максимальное значение: {max}");
    }
}