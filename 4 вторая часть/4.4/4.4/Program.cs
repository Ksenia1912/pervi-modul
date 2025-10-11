using System;

// Интерфейс книги
interface IBook
{
    bool IsAvailable();
    void Borrow();
    void Return();
}

// Обычная книга
class Book : IBook
{
    public string Title;
    public string Author;
    public bool IsBorrowed;

    public bool IsAvailable()
    {
        return !IsBorrowed;
    }

    public void Borrow()
    {
        if (!IsBorrowed)
        {
            IsBorrowed = true;
            Console.WriteLine($"Книга '{Title}' выдана");
        }
        else
        {
            Console.WriteLine($"Книга '{Title}' уже выдана");
        }
    }

    public void Return()
    {
        if (IsBorrowed)
        {
            IsBorrowed = false;
            Console.WriteLine($"Книга '{Title}' возвращена");
        }
        else
        {
            Console.WriteLine($"Книга '{Title}' уже в библиотеке");
        }
    }
}

// Учебник
class Textbook : IBook
{
    public string Title;
    public string Author;
    public string Subject;
    public bool IsBorrowed;

    public bool IsAvailable()
    {
        return !IsBorrowed;
    }

    public void Borrow()
    {
        if (!IsBorrowed)
        {
            IsBorrowed = true;
            Console.WriteLine($"Учебник '{Title}' по предмету {Subject} выдан");
        }
        else
        {
            Console.WriteLine($"Учебник '{Title}' уже выдан");
        }
    }

    public void Return()
    {
        if (IsBorrowed)
        {
            IsBorrowed = false;
            Console.WriteLine($"Учебник '{Title}' возвращен");
        }
        else
        {
            Console.WriteLine($"Учебник '{Title}' уже в библиотеке");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Библиотека:");

        // Обычная книга
        Book book1 = new Book();
        book1.Title = "Кровавая корона";
        book1.Author = "Шуи Цянчэн";
        book1.IsBorrowed = false;

        // Учебник
        Textbook textbook1 = new Textbook();
        textbook1.Title = "Математика";
        textbook1.Author = "Иванов";
        textbook1.Subject = "Математика";
        textbook1.IsBorrowed = false;

        // Проверяем доступность
        Console.WriteLine($"Книга '{book1.Title}' доступна: {book1.IsAvailable()}");
        Console.WriteLine($"Учебник '{textbook1.Title}' доступен: {textbook1.IsAvailable()}");

        // Выдаем книги
        book1.Borrow();
        textbook1.Borrow();

        // Пытаемся выдать снова
        book1.Borrow();

        // Возвращаем книгу
        book1.Return();

        // Проверяем снова
        Console.WriteLine($"Книга '{book1.Title}' доступна: {book1.IsAvailable()}");
    }
}