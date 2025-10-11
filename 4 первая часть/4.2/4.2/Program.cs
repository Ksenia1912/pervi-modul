using System;

// Интерфейс товара
interface IProduct
{
    double GetPrice();
    int GetStock();
}

// Класс продукта
class Product : IProduct
{
    public string Name;
    public double Price;
    public int Stock;

    public double GetPrice()
    {
        return Price;
    }

    public int GetStock()
    {
        return Stock;
    }
}

// Класс продукта со скидкой
class DiscountProduct : IProduct
{
    public string Name;
    public double Price;
    public int Stock;
    public double Discount; // скидка в процентах

    public double GetPrice()
    {
        return Price * (1 - Discount / 100);
    }

    public int GetStock()
    {
        return Stock;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Товары в магазине:");

        // Обычный товар
        Product milk = new Product();
        milk.Name = "Молоко";
        milk.Price = 80;
        milk.Stock = 10;
        Console.WriteLine($"{milk.Name}: цена = {milk.GetPrice()}, остаток = {milk.GetStock()}");

        // Товар со скидкой
        DiscountProduct bread = new DiscountProduct();
        bread.Name = "Хлеб";
        bread.Price = 50;
        bread.Stock = 5;
        bread.Discount = 20; // 20% скидка
        Console.WriteLine($"{bread.Name}: цена со скидкой = {bread.GetPrice()}, остаток = {bread.GetStock()}");

        // Еще один товар
        Product cheese = new Product();
        cheese.Name = "Сыр";
        cheese.Price = 300;
        cheese.Stock = 3;
        Console.WriteLine($"{cheese.Name}: цена = {cheese.GetPrice()}, остаток = {cheese.GetStock()}");
    }
}