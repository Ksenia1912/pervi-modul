using System;

class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    public Car(string brand, string model, int year, decimal price)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }

    // Метод для расчета стоимости со скидкой
    public decimal CalculatePriceWithDiscount(decimal discountPercent)
    {
        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentException("Скидка должна быть от 0 до 100%");

        return Price * (1 - discountPercent / 100);
    }

    // Метод для расчета стоимости с НДС
    public decimal CalculatePriceWithVAT(decimal vatPercent = 20)
    {
        return Price * (1 + vatPercent / 100);
    }

    // Метод для расчета стоимости со скидкой и НДС
    public decimal CalculateFinalPrice(decimal discountPercent, decimal vatPercent = 20)
    {
        decimal priceAfterDiscount = CalculatePriceWithDiscount(discountPercent);
        return priceAfterDiscount * (1 + vatPercent / 100);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Автомобиль: {Brand} {Model}");
        Console.WriteLine($"Год выпуска: {Year}");
        Console.WriteLine($"Базовая цена: {Price:C}");
    }
}

class Program
{
    static void Main()
    {
        Car car1 = new Car("Toyota", "Camry", 2022, 2500000);
        Car car2 = new Car("BMW", "X5", 2023, 5000000);

        Console.WriteLine("Расчет стоимости автомобилей:");
        Console.WriteLine("=============================\n");

        // Первый автомобиль
        car1.DisplayInfo();
        Console.WriteLine($"Цена со скидкой 10%: {car1.CalculatePriceWithDiscount(10):C}");
        Console.WriteLine($"Цена с НДС: {car1.CalculatePriceWithVAT():C}");
        Console.WriteLine($"Итоговая цена (скидка 10% + НДС): {car1.CalculateFinalPrice(10):C}");
        Console.WriteLine();

        // Второй автомобиль
        car2.DisplayInfo();
        Console.WriteLine($"Цена со скидкой 15%: {car2.CalculatePriceWithDiscount(15):C}");
        Console.WriteLine($"Цена с НДС: {car2.CalculatePriceWithVAT():C}");
        Console.WriteLine($"Итоговая цена (скидка 15% + НДС): {car2.CalculateFinalPrice(15):C}");
    }
}