using System;
using System.Linq;

struct Student
{
    public string LastNameAndInitials;
    public string GroupNumber;
    public int[] Grades; // массив из 5 оценок

    public Student(string lastName, string group, int[] grades)
    {
        LastNameAndInitials = lastName;
        GroupNumber = group;
        Grades = grades;
    }

    // Метод для вычисления среднего балла
    public double GetAverageGrade()
    {
        return Grades.Average();
    }

    // Метод для проверки, все ли оценки 4 или 5
    public bool HasOnlyGoodGrades()
    {
        return Grades.All(grade => grade == 4 || grade == 5);
    }

    public void DisplayInfo()
    {
        Console.Write($"{LastNameAndInitials,-20} {GroupNumber,-10} ");
        foreach (int grade in Grades)
        {
            Console.Write($"{grade} ");
        }
        Console.WriteLine($"| Средний: {GetAverageGrade():F2}");
    }
}

class Program
{
    static void Main()
    {
        // Создание массива студентов
        Student[] students = new Student[10];

        // Инициализация студентов
        students[0] = new Student("Овечкин А.И.", "ПОИС62", new int[] { 5, 4, 5, 5, 4 });
        students[1] = new Student("Быков Б.С.", "ПОИС62", new int[] { 3, 4, 3, 5, 4 });
        students[2] = new Student("Козий В.К.", "ПОИС62", new int[] { 5, 5, 5, 5, 5 });
        students[3] = new Student("Петухова Д.М.", "ПОИС62", new int[] { 4, 4, 4, 4, 4 });
        students[4] = new Student("Курицев Е.П.", "ПОИС62", new int[] { 3, 3, 4, 3, 4 });
        students[5] = new Student("Собычкин Ж.Р.", "ПОИС62", new int[] { 5, 4, 5, 4, 5 });
        students[6] = new Student("Петров З.Т.", "ПОИС62", new int[] { 4, 5, 4, 5, 4 });
        students[7] = new Student("Котов Ю.У.", "ПОИС62", new int[] { 3, 4, 3, 3, 4 });
        students[8] = new Student("Лошаднева К.Ч.", "ПОИС62", new int[] { 5, 5, 4, 5, 5 });
        students[9] = new Student("Свинев Л.Я.", "ПОИС62", new int[] { 4, 4, 5, 4, 4 });

        // Сортировка по возрастанию среднего балла
        for (int i = 0; i < students.Length - 1; i++)
        {
            for (int j = i + 1; j < students.Length; j++)
            {
                if (students[i].GetAverageGrade() > students[j].GetAverageGrade())
                {
                    Student temp = students[i];
                    students[i] = students[j];
                    students[j] = temp;
                }
            }
        }

        // Вывод отсортированного списка
        Console.WriteLine("Список студентов (отсортирован по среднему баллу):");
        Console.WriteLine("Фамилия и инициалы   Группа    Оценки         Средний балл");

        foreach (Student student in students)
        {
            student.DisplayInfo();
        }

        // Вывод студентов с оценками только 4 и 5
        Console.WriteLine("\nСтуденты с оценками только 4 и 5:");

        var excellentStudents = students.Where(s => s.HasOnlyGoodGrades());

        if (excellentStudents.Any())
        {
            foreach (Student student in excellentStudents)
            {
                Console.WriteLine($"{student.LastNameAndInitials} - {student.GroupNumber}");
            }
        }
        else
        {
            Console.WriteLine("Таких студентов нет");
        }

        Console.WriteLine($"\nВсего студентов: {students.Length}");
        Console.WriteLine($"Из них с оценками только 4 и 5: {excellentStudents.Count()}");
    }
}