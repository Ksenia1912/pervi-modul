using System;

class Author
{
    public string Name { get; set; }
    public int BirthYear { get; set; }

    public Author(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Автор: {Name} (год рождения: {BirthYear})");
    }
}

class Book
{
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public Author Author { get; set; } // Композиция

    public Book(string title, int publicationYear, Author author)
    {
        Title = title;
        PublicationYear = publicationYear;
        Author = author;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Книга: \"{Title}\"");
        Console.WriteLine($"Год издания: {PublicationYear}");
        Author.DisplayInfo();
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Создание авторов
        Author author1 = new Author("Прист", 1988);
        Author author2 = new Author("Шуи Цянчэн", 1986);
        Author author3 = new Author("Митбан", 0000);

        // Создание книг с авторами
        Book book1 = new Book("Лю Яо: Возрождение клана Фу Яо", 2014, author1);
        Book book2 = new Book("Кровавая корона", 2017, author2);
        Book book3 = new Book("Седьмой лорд", 2010, author1);
        Book book4 = new Book("Остатки грязи", 2018, author3);

        Book[] books = { book1, book2, book3, book4 };

        Console.WriteLine("Библиотека книг:");

        foreach (Book book in books)
        {
            book.DisplayInfo();
        }

       
    }
}