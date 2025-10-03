using System;

// Базовый класс
abstract class Shape
{
    public abstract double Area();
    public abstract double Perimeter();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Площадь: {Area():F2}, Периметр: {Perimeter():F2}");
    }
}

// Производный класс Circle
class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }

    public override double Perimeter()
    {
        return 2 * Math.PI * Radius;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Круг (радиус = {Radius}): Площадь = {Area():F2}, Периметр = {Perimeter():F2}");
    }
}

// Производный класс Rectangle
class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double Area()
    {
        return Width * Height;
    }

    public override double Perimeter()
    {
        return 2 * (Width + Height);
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Прямоугольник ({Width} x {Height}): Площадь = {Area():F2}, Периметр = {Perimeter():F2}");
    }
}

class Program
{
    static void Main()
    {
        // Создание объектов разных фигур
        Shape circle = new Circle(5);
        Shape rectangle = new Rectangle(4, 6);
        Shape circle2 = new Circle(3.5);
        Shape rectangle2 = new Rectangle(2.5, 8);

        // Массив фигур для демонстрации полиморфизма
        Shape[] shapes = { circle, rectangle, circle2, rectangle2 };

        Console.WriteLine("Информация о геометрических фигурах:");

        foreach (Shape shape in shapes)
        {
            shape.DisplayInfo();
        }
    }
}