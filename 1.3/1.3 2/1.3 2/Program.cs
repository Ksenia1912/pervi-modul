using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Введите максимальную сумму элементов: ");
        int maxSum = int.Parse(Console.ReadLine());


        Random random = new Random();
        List<int> array = new List<int>();
        int currentSum = 0;

        // Добавление элементов, пока сумма не превысит максимальную
        while (currentSum <= maxSum)
        {
            int newElement = random.Next(1, 10); // случайное число от 1 до 9
            int potentialSum = currentSum + newElement;

            // Если добавление нового элемента превысит максимальную сумму, останjdrf
            if (potentialSum > maxSum)
                break;

            array.Add(newElement);
            currentSum = potentialSum;
        }

        Console.Write("Созданный массив: ");
        for (int i = 0; i < array.Count; i++)
        {
            Console.Write(array[i]);
            if (i < array.Count - 1)
                Console.Write(" + ");
        }
        Console.WriteLine($" = {currentSum}");
       
    }
}