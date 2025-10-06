using System;

// Делегат для вычисления площади
public delegate double CalculateAreaDelegate();

// Базовый класс Фигура
abstract class Shape
{
    public abstract double CalculateArea();
}

// Класс Круг
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
}

// Класс Прямоугольник
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
}

// Класс Треугольник
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
}

class Program
{
    static void Main()
    {
        // Создание фигур
        Circle circle = new Circle(5);
        Rectangle rectangle = new Rectangle(4, 6);
        Triangle triangle = new Triangle(3, 4);

        // Создание делегатов для каждой фигуры
        CalculateAreaDelegate circleArea = circle.CalculateArea;
        CalculateAreaDelegate rectangleArea = rectangle.CalculateArea;
        CalculateAreaDelegate triangleArea = triangle.CalculateArea;

        // Массив делегатов
        CalculateAreaDelegate[] areaDelegates = { circleArea, rectangleArea, triangleArea };
        Shape[] shapes = { circle, rectangle, triangle };
        string[] shapeNames = { "Круг", "Прямоугольник", "Треугольник" };

        Console.WriteLine("Вычисление площадей фигур с использованием делегатов:");

        // Вызов делегатов
        for (int i = 0; i < areaDelegates.Length; i++)
        {
            double area = areaDelegates[i]();
            Console.WriteLine($"{shapeNames[i]}: Площадь = {area:F2}");
        }

    }
}