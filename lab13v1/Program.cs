
using System;
using System.Collections.Generic;

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Grade { get; set; }

    public Student(string name, int age, double grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }

    public override string ToString()
    {
        return $"{Name}, вік: {Age}, оцінка: {Grade}";
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== LABORATORY WORK 13 ===");
        Console.WriteLine("Variant 1\n");

        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

        Console.WriteLine("1. MAP (int):");
        var squares = FunctionalUtils.Map(numbers, x => x * x);
        FunctionalUtils.ForEach(squares, x => Console.Write(x + " "));
        Console.WriteLine("\n");

        Console.WriteLine("2. FILTER (int):");
        var evenNumbers = FunctionalUtils.Filter(numbers, x => x % 2 == 0);
        FunctionalUtils.ForEach(evenNumbers, x => Console.Write(x + " "));
        Console.WriteLine("\n");

        Console.WriteLine("3. REDUCE (int):");
        int sum = FunctionalUtils.Reduce(numbers, 0, (total, x) => total + x);
        Console.WriteLine($"Сума: {sum}\n");

        Console.WriteLine("4. FOREACH (int):");
        FunctionalUtils.ForEach(numbers, x => Console.WriteLine($"Число: {x}"));
        Console.WriteLine();

        Console.WriteLine("5. ANY (int):");
        bool hasBigNumber = FunctionalUtils.Any(numbers, x => x > 4);
        Console.WriteLine($"Є число більше 4: {hasBigNumber}\n");

        List<string> names = new List<string>
        {
            "Andriy", "Ivan", "Oleh", "Taras"
        };

        Console.WriteLine("=== STRING ===");

        var upperNames = FunctionalUtils.Map(names, name => name.ToUpper());
        FunctionalUtils.ForEach(upperNames, name => Console.WriteLine(name));

        var longNames = FunctionalUtils.Filter(names, name => name.Length > 4);

        Console.WriteLine("Довгі імена:");
        FunctionalUtils.ForEach(longNames, name => Console.WriteLine(name));

        string allNames = FunctionalUtils.Reduce(
            names, "",
            (result, name) => result == "" ? name : result + ", " + name);

        Console.WriteLine($"Усі імена: {allNames}");

        bool hasTaras = FunctionalUtils.Any(names, name => name == "Taras");
        Console.WriteLine($"Є Taras: {hasTaras}\n");

        List<Student> students = new List<Student>
        {
            new Student("Alex", 18, 90),
            new Student("Max", 19, 75),
            new Student("Daniel", 18, 95)
        };

        Console.WriteLine("=== STUDENTS ===");

        var studentNames = FunctionalUtils.Map(students, student => student.Name);

        Console.WriteLine("Імена студентів:");
        FunctionalUtils.ForEach(studentNames, name => Console.WriteLine(name));

        var successfulStudents = FunctionalUtils.Filter(
            students, student => student.Grade >= 80);

        Console.WriteLine("\nСтуденти з оцінкою від 80:");
        FunctionalUtils.ForEach(successfulStudents, student => Console.WriteLine(student));

        double average = FunctionalUtils.Reduce(
            students, 0.0,
            (total, student) => total + student.Grade) / students.Count;

        Console.WriteLine($"\nСередня оцінка: {average:F2}");

        bool hasExcellentStudent = FunctionalUtils.Any(
            students, student => student.Grade >= 90);

        Console.WriteLine($"Є студент з оцінкою від 90: {hasExcellentStudent}");

        Console.WriteLine("\n=== END OF PROGRAM ===");
    }
}
