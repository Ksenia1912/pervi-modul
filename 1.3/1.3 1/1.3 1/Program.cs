using System;

class Program
{
    // Статический метод для вычисления НОД
    static int NOD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static void Main()
    {
        Console.WriteLine("Введите неотрицательную обыкновенную дробь:");

        Console.Write("Числитель: ");
        int numerator = int.Parse(Console.ReadLine());

        Console.Write("Знаменатель: ");
        int denominator = int.Parse(Console.ReadLine());

        if (denominator <= 0)
        {
            Console.WriteLine("Ошибка: знаменатель должен быть положительным числом!");
            return;
        }

        if (numerator < 0)
        {
            Console.WriteLine("Ошибка: числитель должен быть неотрицательным!");
            return;
        }

        // Вычисляем НОД числителя и знаменателя
        int gcd = NOD(numerator, denominator);

        // Сокращаем дробь
        int reducedNumerator = numerator / gcd;
        int reducedDenominator = denominator / gcd;

        // Выводим результат
        Console.WriteLine($"Исходная дробь: {numerator}/{denominator}");
        Console.WriteLine($"НОД({numerator}, {denominator}) = {gcd}");

        if (gcd == 1)
        {
            Console.WriteLine("Дробь уже несократима");
        }
        else
        {
            Console.WriteLine($"Сокращенная дробь: {reducedNumerator}/{reducedDenominator}");
        }
    }
}