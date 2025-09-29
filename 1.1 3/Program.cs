using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите ваше имя: ");
        string Name = Console.ReadLine();

        Console.Write("Введите вашу фамилию: ");
        string Familia = Console.ReadLine();

        Console.WriteLine($"Результат: {Familia}, {Name}");
    }
}