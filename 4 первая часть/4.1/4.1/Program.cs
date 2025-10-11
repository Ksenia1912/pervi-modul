using System;

// Интерфейс
interface IShape
{
    double GetArea();
    double GetPerimeter();
}

// Круг
class Circle : IShape
{
    public double Radius;

    public double GetArea()
    {
        return 3.14 * Radius * Radius;
    }

    public double GetPerimeter()
    {
        return 2 * 3.14 * Radius;
    }
}

// Прямоугольник
class Rectangle : IShape
{
    public double Width;
    public double Height;

    public double GetArea()
    {
        return Width * Height;
    }

    public double GetPerimeter()
    {
        return 2 * (Width + Height);
    }
}

// Треугольник
class Triangle : IShape
{
    public double Side1;
    public double Side2;
    public double Side3;

    public double GetArea()
    {
        return 0.5 * Side1 * Side2;
    }

    public double GetPerimeter()
    {
        return Side1 + Side2 + Side3;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Фигуры:");

        // Создание круга
        Circle circle = new Circle();
        circle.Radius = 5;
        Console.WriteLine($"Круг: площадь = {circle.GetArea()}, периметр = {circle.GetPerimeter()}");

        // Создание прямоугольника
        Rectangle rect = new Rectangle();
        rect.Width = 4;
        rect.Height = 6;
        Console.WriteLine($"Прямоугольник: площадь = {rect.GetArea()}, периметр = {rect.GetPerimeter()}");

        // Создание треугольника
        Triangle triangle = new Triangle();
        triangle.Side1 = 3;
        triangle.Side2 = 4;
        triangle.Side3 = 5;
        Console.WriteLine($"Треугольник: площадь = {triangle.GetArea()}, периметр = {triangle.GetPerimeter()}");
    }
}