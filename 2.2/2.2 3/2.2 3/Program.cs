using System;

// Абстрактный класс
abstract class Shape
{
    public abstract double CalculateArea();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Площадь: {CalculateArea():F2}");
    }
}

// Класс Circle
class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Круг (радиус = {Radius}): Площадь = {CalculateArea():F2}");
    }
}

// Класс Rectangle
class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double CalculateArea()
    {
        return Width * Height;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Прямоугольник ({Width} x {Height}): Площадь = {CalculateArea():F2}");
    }
}

// Класс Triangle
class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public Triangle(double triangleBase, double height)
    {
        Base = triangleBase;
        Height = height;
    }

    public override double CalculateArea()
    {
        return 0.5 * Base * Height;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Треугольник (основание = {Base}, высота = {Height}): Площадь = {CalculateArea():F2}");
    }
}

class Program
{
    static void Main()
    {
        // Создание объектов разных фигур
        Shape[] shapes = {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4),
            new Circle(2.5),
            new Rectangle(10, 8),
            new Triangle(5, 7)
        };

        Console.WriteLine("Площади геометрических фигур:");

        // Вывод информации о всех фигурах
        foreach (Shape shape in shapes)
        {
            shape.DisplayInfo();
        }

    }
}