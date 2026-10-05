using System;
using System.Collections.Generic;
using System.Linq;

class Playlist
{
    private List<string> _songs = new List<string>();

    public int Count
    {
        get { return _songs.Count; }
    }

    public void Add(string song)
    {
        _songs.Add(song);
    }

    public void Shuffle()
    {
        Random rnd = new Random();
        for (int i = _songs.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            string temp = _songs[i];
            _songs[i] = _songs[j];
            _songs[j] = temp;
        }
    }

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _songs.Count)
                throw new IndexOutOfRangeException("Немає пісні з таким номером");
            return _songs[index];
        }
        set
        {
            if (index < 0 || index >= _songs.Count)
                throw new IndexOutOfRangeException("Немає пісні з таким номером");
            _songs[index] = value;
        }
    }

    public static Playlist operator +(Playlist a, Playlist b)
    {
        Playlist result = new Playlist();
        foreach (string song in a._songs) result.Add(song);
        foreach (string song in b._songs) result.Add(song);
        return result;
    }

    public static bool operator ==(Playlist a, Playlist b)
    {
        if (a is null || b is null)
            return a is null && b is null;
        return a._songs.SequenceEqual(b._songs);
    }

    public static bool operator !=(Playlist a, Playlist b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Playlist other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (string song in _songs)
            hash = hash * 31 + song.GetHashCode();
        return hash;
    }

    public override string ToString()
    {
        if (_songs.Count == 0) return "(плейлист порожній)";
        return string.Join(", ", _songs);
    }
}

class Program
{
    static void Main()
    {
        Playlist p1 = new Playlist();
        p1.Add("Song A");
        p1.Add("Song B");
        p1.Add("Song C");

        Playlist p2 = new Playlist();
        p2.Add("Song D");
        p2.Add("Song E");

        Playlist p3 = new Playlist();
        p3.Add("Song A");
        p3.Add("Song B");
        p3.Add("Song C");

        Console.WriteLine("p1: " + p1);
        Console.WriteLine("p2: " + p2);
        Console.WriteLine("Кількість пісень у p1: " + p1.Count);

        Console.WriteLine("\np1[0] = " + p1[0]);
        Console.WriteLine("p1[2] = " + p1[2]);

        p1[1] = "New Song";
        Console.WriteLine("Після p1[1] = New Song: " + p1);
        p1[1] = "Song B";

        try
        {
            Console.WriteLine(p1[10]);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }

        Playlist all = p1 + p2;
        Console.WriteLine("\np1 + p2: " + all);
        Console.WriteLine("Кількість у p1 + p2: " + all.Count);

        Console.WriteLine("\np1 == p3: " + (p1 == p3));
        Console.WriteLine("p1 == p2: " + (p1 == p2));
        Console.WriteLine("p1 != p2: " + (p1 != p2));
        Console.WriteLine("p1.Equals(p3): " + p1.Equals(p3));
        Console.WriteLine("Хеші p1 і p3 однакові: " + (p1.GetHashCode() == p3.GetHashCode()));

        // Shuffle
        all.Shuffle();
        Console.WriteLine("\nПісля Shuffle: " + all);
    }
}
