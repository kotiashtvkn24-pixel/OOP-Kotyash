using System;

class Book
{
    private string _title;
    private int _pages;
    private int _currentPage;

    public string Title
    {
        get { return _title; }
    }

    public int Pages
    {
        get { return _pages; }
    }

    public int CurrentPage
    {
        get { return _currentPage; }
        set { _currentPage = value; }
    }

    public Book(string title, int pages)
    {
        _title = title;
        _pages = pages;
        _currentPage = 0;
    }

    public void ReadPages(int count)
    {
        _currentPage += count;
        if (_currentPage > _pages)
        {
            _currentPage = _pages;
        }
    }

    public double GetProgress()
    {
        return (double)_currentPage / _pages * 100;
    }
}

class Recipe
{
    private string _name;
    private int _cookingMinutes;
    private int _servings;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int CookingMinutes
    {
        get { return _cookingMinutes; }
    }

    public Recipe(string name, int cookingMinutes, int servings)
    {
        _name = name;
        _cookingMinutes = cookingMinutes;
        _servings = servings;
    }

    public double GetMinutesPerServing()
    {
        return (double)_cookingMinutes / _servings;
    }

    public bool IsQuick()
    {
        return _cookingMinutes <= 30;
    }
}

class Ticket
{
    private string _movie;
    private decimal _price;
    private int _seat;

    public string Movie
    {
        get { return _movie; }
    }

    public int Seat
    {
        get { return _seat; }
        set { _seat = value; }
    }

    public Ticket(string movie, decimal price, int seat)
    {
        _movie = movie;
        _price = price;
        _seat = seat;
    }

    public decimal GetPriceWithDiscount(decimal percent)
    {
        return _price * (1 - percent / 100);
    }
}

class Program
{
    static void Main()
    {
        Book book = new Book("Кобзар", 300);
        book.ReadPages(120);
        Console.WriteLine($"Книга '{book.Title}': прочитано {book.CurrentPage} з {book.Pages} стор. ({book.GetProgress():F0}%)");

        Recipe recipe = new Recipe("Борщ", 90, 6);
        Console.WriteLine($"Рецепт '{recipe.Name}': {recipe.CookingMinutes} хв, {recipe.GetMinutesPerServing():F0} хв на порцію");
        Console.WriteLine("Швидкий рецепт: " + (recipe.IsQuick() ? "так" : "ні"));

        Ticket ticket = new Ticket("Дюна", 200m, 12);
        Console.WriteLine($"Квиток на '{ticket.Movie}', місце {ticket.Seat}, зі знижкою 10%: {ticket.GetPriceWithDiscount(10):F0} грн");
    }
}