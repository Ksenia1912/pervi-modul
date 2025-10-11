using System;

// Интерфейс студента
interface IStudent
{
    double GetAverageGrade();
    string GetCourseInfo();
}

// Студент бакалавриата
class BachelorStudent : IStudent
{
    public string Name;
    public int Course;
    public double[] Grades;

    public double GetAverageGrade()
    {
        double sum = 0;
        for (int i = 0; i < Grades.Length; i++)
        {
            sum += Grades[i];
        }
        return sum / Grades.Length;
    }

    public string GetCourseInfo()
    {
        return $"Бакалавр {Course} курса";
    }
}

// Студент магистратуры
class MasterStudent : IStudent
{
    public string Name;
    public int Course;
    public double[] Grades;

    public double GetAverageGrade()
    {
        double sum = 0;
        for (int i = 0; i < Grades.Length; i++)
        {
            sum += Grades[i];
        }
        return sum / Grades.Length;
    }

    public string GetCourseInfo()
    {
        return $"Магистр {Course} курса";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Студенты:");

        // Студент бакалавриата
        BachelorStudent student1 = new BachelorStudent();
        student1.Name = "Александр";
        student1.Course = 2;
        student1.Grades = new double[] { 3, 7, 8, 2 };
        Console.WriteLine($"{student1.Name}: {student1.GetCourseInfo()}, средний балл = {student1.GetAverageGrade():F2}");

        // Студент магистратуры
        MasterStudent student2 = new MasterStudent();
        student2.Name = "Светлана";
        student2.Course = 1;
        student2.Grades = new double[] { 10, 8, 9, 10 };
        Console.WriteLine($"{student2.Name}: {student2.GetCourseInfo()}, средний балл = {student2.GetAverageGrade():F2}");

        // Еще один студент
        BachelorStudent student3 = new BachelorStudent();
        student3.Name = "Алексей";
        student3.Course = 4;
        student3.Grades = new double[] { 8, 7, 6, 7.1 };
        Console.WriteLine($"{student3.Name}: {student3.GetCourseInfo()}, средний балл = {student3.GetAverageGrade():F2}");
    }
}