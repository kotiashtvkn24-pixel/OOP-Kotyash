using lab12v1;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var words = new List<string> { "apple", "banana", "kiwi", "cherry", "fig", "grape" };
var people = new List<Person>
{
    new Person("Олена", 19, 12000),
    new Person("Іван", 25, 28000),
    new Person("Марія", 31, 35000),
    new Person("Петро", 17, 0),
    new Person("Софія", 22, 18000)
};

Console.WriteLine("=== Map ===");
var squares = FunctionalUtils.Map(numbers, x => x * x);
Console.WriteLine("int -> квадрат: " + string.Join(", ", squares));
var lengths = FunctionalUtils.Map(words, w => w.Length);
Console.WriteLine("string -> довжина: " + string.Join(", ", lengths));
var names = FunctionalUtils.Map(people, p => p.Name.ToUpper());
Console.WriteLine("Person -> ім'я: " + string.Join(", ", names));

Console.WriteLine();
Console.WriteLine("=== Filter ===");
var even = FunctionalUtils.Filter(numbers, x => x % 2 == 0);
Console.WriteLine("парні: " + string.Join(", ", even));
var longWords = FunctionalUtils.Filter(words, w => w.Length > 4);
Console.WriteLine("слова довші за 4: " + string.Join(", ", longWords));
var adults = FunctionalUtils.Filter(people, p => p.Age >= 18);
Console.WriteLine("повнолітні: " + string.Join("; ", adults));

Console.WriteLine();
Console.WriteLine("=== Reduce ===");
var sum = FunctionalUtils.Reduce(numbers, 0, (acc, x) => acc + x);
Console.WriteLine("сума чисел: " + sum);
var joined = FunctionalUtils.Reduce(words, "", (acc, w) => acc == "" ? w : acc + "-" + w);
Console.WriteLine("рядок через дефіс: " + joined);
var totalSalary = FunctionalUtils.Reduce(people, 0.0, (acc, p) => acc + p.Salary);
Console.WriteLine("сума зарплат: " + totalSalary);

Console.WriteLine();
Console.WriteLine("=== ForEach ===");
FunctionalUtils.ForEach(numbers, x => Console.Write(x * 10 + " "));
Console.WriteLine();
FunctionalUtils.ForEach(words, w => Console.WriteLine("слово: " + w));
FunctionalUtils.ForEach(people, p => Console.WriteLine("людина: " + p));

Console.WriteLine();
Console.WriteLine("=== Any ===");
Console.WriteLine("є число > 9: " + FunctionalUtils.Any(numbers, x => x > 9));
Console.WriteLine("є число > 10: " + FunctionalUtils.Any(numbers, x => x > 10));
Console.WriteLine("є слово \"kiwi\": " + FunctionalUtils.Any(words, w => w == "kiwi"));
Console.WriteLine("є слово \"mango\": " + FunctionalUtils.Any(words, w => w == "mango"));
Console.WriteLine("є неповнолітні: " + FunctionalUtils.Any(people, p => p.Age < 18));
Console.WriteLine("є зарплата > 50000: " + FunctionalUtils.Any(people, p => p.Salary > 50000));