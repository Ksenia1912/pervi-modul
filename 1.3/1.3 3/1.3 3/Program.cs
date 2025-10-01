using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размер квадратной матрицы: ");
        int size = int.Parse(Console.ReadLine());

        if (size <= 0)
        {
            Console.WriteLine("Размер матрицы должен быть положительным!");
            return;
        }

        Random random = new Random();
        int[,] matrix = new int[size, size];

        // Заполнение матрицы случайными числами от -50 до 50
        Console.WriteLine("\nИсходная матрица:");
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = random.Next(-50, 51); // от -50 до 50 включительно
                Console.Write(matrix[i, j].ToString().PadLeft(4));
            }
            Console.WriteLine();
        }

        // Вычисление сумм строк
        int[] rowSums = new int[size];
        for (int i = 0; i < size; i++)
        {
            int sum = 0;
            for (int j = 0; j < size; j++)
            {
                sum += matrix[i, j];
            }
            rowSums[i] = sum;
        }

        // Вывод сумм строк
        Console.WriteLine("\nСуммы строк:");
        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Строка {i}: {rowSums[i]}");
        }

        // Сортировка строк по возрастанию суммы (пузырьковая сортировка)
        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; j < size - 1 - i; j++)
            {
                if (rowSums[j] > rowSums[j + 1])
                {
                    // Обмен сумм
                    int tempSum = rowSums[j];
                    rowSums[j] = rowSums[j + 1];
                    rowSums[j + 1] = tempSum;

                    // Обмен строк в матрице
                    for (int k = 0; k < size; k++)
                    {
                        int temp = matrix[j, k];
                        matrix[j, k] = matrix[j + 1, k];
                        matrix[j + 1, k] = temp;
                    }
                }
            }
        }

        // Вывод отсортированной матрицы
        Console.WriteLine("\nМатрица после сортировки строк по возрастанию суммы:");
        for (int i = 0; i < size; i++)
        {
            int currentSum = 0;
            for (int j = 0; j < size; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(4));
                currentSum += matrix[i, j];
            }
            Console.WriteLine($"  ");
        } 
    }
}