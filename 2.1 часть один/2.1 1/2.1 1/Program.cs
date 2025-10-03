using System;

class Person
{
    // Поля класса
    private string name;
    private int age;
    private string address;

    // Конструктор
    public Person(string name, int age, string address)
    {
        this.name = name;
        this.age = age;
        this.address = address;
    }

    // Методы для установки значений
    public void SetName(string name) => this.name = name;
    public void SetAge(int age) => this.age = age;
    public void SetAddress(string address) => this.address = address;

    // Методы для получения значений
    public string GetName() => name;
    public int GetAge() => age;
    public string GetAddress() => address;

    // Метод для вывода информации
    public void DisplayInfo()
    {
        Console.WriteLine($"Имя: {name}");
        Console.WriteLine($"Возраст: {age}");
        Console.WriteLine($"Адрес: {address}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Создание объектов Person
        Person person1 = new Person("Ксения", 17, "ул. Грицевца, д. 388");
        Person person2 = new Person("Екатерина", 16, "пр. Мира, д. 25");
        Person person3 = new Person("Алия", 18, "ул. Садовая, д. 15");

        // Вывод информации о людях
        Console.WriteLine("Информация о людях:");
        person1.DisplayInfo();
        person2.DisplayInfo();
        person3.DisplayInfo();

        // Изменение данных одного из объектов
        person2.SetAge(17);
        person2.SetAddress("ул. Новая, д. 5");

        Console.WriteLine("После изменения данных:");
        person2.DisplayInfo();
    }
}