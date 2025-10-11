using System;

// Интерфейс для рисования
interface IDrawing
{
    void Draw();
    void Clear();
}

// Класс для рисования линий
class LineDrawing : IDrawing
{
    public int X1, Y1, X2, Y2;

    public void Draw()
    {
        Console.WriteLine($"Рисую линию от ({X1},{Y1}) до ({X2},{Y2})");
    }

    public void Clear()
    {
        Console.WriteLine("Стираю линию");
    }
}

// Класс для рисования кругов
class CircleDrawing : IDrawing
{
    public int X, Y, Radius;

    public void Draw()
    {
        Console.WriteLine($"Рисую круг в точке ({X},{Y}) с радиусом {Radius}");
    }

    public void Clear()
    {
        Console.WriteLine("Стираю круг");
    }
}

// Класс для рисования прямоугольников
class RectangleDrawing : IDrawing
{
    public int X, Y, Width, Height;

    public void Draw()
    {
        Console.WriteLine($"Рисую прямоугольник в ({X},{Y}) размером {Width}x{Height}");
    }

    public void Clear()
    {
        Console.WriteLine("Стираю прямоугольник");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Рисование на холсте:");

        // Рисуем линию
        LineDrawing line = new LineDrawing();
        line.X1 = 10;
        line.Y1 = 10;
        line.X2 = 50;
        line.Y2 = 50;
        line.Draw();

        // Рисуем круг
        CircleDrawing circle = new CircleDrawing();
        circle.X = 30;
        circle.Y = 30;
        circle.Radius = 20;
        circle.Draw();

        // Рисуем прямоугольник
        RectangleDrawing rect = new RectangleDrawing();
        rect.X = 5;
        rect.Y = 5;
        rect.Width = 40;
        rect.Height = 30;
        rect.Draw();

        // Стираем
        Console.WriteLine("\nСтираем все:");
        line.Clear();
        circle.Clear();
        rect.Clear();

        // Рисуем еще что-то
        Console.WriteLine("\nРисуем новую фигуру:");
        LineDrawing line2 = new LineDrawing();
        line2.X1 = 0;
        line2.Y1 = 0;
        line2.X2 = 100;
        line2.Y2 = 100;
        line2.Draw();
    }
}