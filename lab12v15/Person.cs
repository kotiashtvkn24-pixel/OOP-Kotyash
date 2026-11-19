namespace lab12v1;

public class Person
{
    public string Name { get; }
    public int Age { get; }
    public double Salary { get; }

    public Person(string name, int age, double salary)
    {
        Name = name;
        Age = age;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{Name}, {Age} р., {Salary} грн";
    }
}
