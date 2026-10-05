using System;

class Matrix2x2
{
    private double _m00, _m01, _m10, _m11;

    public double M00 { get { return _m00; } set { Check(value); _m00 = value; } }
    public double M01 { get { return _m01; } set { Check(value); _m01 = value; } }
    public double M10 { get { return _m10; } set { Check(value); _m10 = value; } }
    public double M11 { get { return _m11; } set { Check(value); _m11 = value; } }

    private static void Check(double value)
    {
        if (double.IsNaN(value))
            throw new ArgumentException("Значення не може бути NaN");
    }

    public Matrix2x2(double m00, double m01, double m10, double m11)
    {
        M00 = m00;
        M01 = m01;
        M10 = m10;
        M11 = m11;
    }

    public static Matrix2x2 Identity
    {
        get { return new Matrix2x2(1, 0, 0, 1); }
    }

    public double this[int row, int col]
    {
        get
        {
            if (row == 0 && col == 0) return _m00;
            if (row == 0 && col == 1) return _m01;
            if (row == 1 && col == 0) return _m10;
            if (row == 1 && col == 1) return _m11;
            throw new IndexOutOfRangeException("Індекси мають бути 0 або 1");
        }
        set
        {
            if (row == 0 && col == 0) M00 = value;
            else if (row == 0 && col == 1) M01 = value;
            else if (row == 1 && col == 0) M10 = value;
            else if (row == 1 && col == 1) M11 = value;
            else throw new IndexOutOfRangeException("Індекси мають бути 0 або 1");
        }
    }

    public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b)
    {
        return new Matrix2x2(a.M00 + b.M00, a.M01 + b.M01,
                             a.M10 + b.M10, a.M11 + b.M11);
    }

    public static Matrix2x2 operator *(Matrix2x2 a, double k)
    {
        return new Matrix2x2(a.M00 * k, a.M01 * k, a.M10 * k, a.M11 * k);
    }

    public static Matrix2x2 operator *(double k, Matrix2x2 a)
    {
        return a * k;
    }

    public static bool operator ==(Matrix2x2 a, Matrix2x2 b)
    {
        if (a is null || b is null)
            return a is null && b is null;
        return a.M00 == b.M00 && a.M01 == b.M01 &&
               a.M10 == b.M10 && a.M11 == b.M11;
    }

    public static bool operator !=(Matrix2x2 a, Matrix2x2 b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Matrix2x2 other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(M00, M01, M10, M11);
    }

    public override string ToString()
    {
        return $"[{M00} {M01}]\n[{M10} {M11}]";
    }
}

class Program
{
    static void Main()
    {
        Matrix2x2 a = new Matrix2x2(1, 2, 3, 4);
        Matrix2x2 b = new Matrix2x2(5, 6, 7, 8);
        Matrix2x2 c = new Matrix2x2(1, 2, 3, 4);

        Console.WriteLine("Матриця a:\n" + a);
        Console.WriteLine("Матриця b:\n" + b);

        a.M00 = 10;
        Console.WriteLine("\nПісля a.M00 = 10:\n" + a);

        try
        {
            a.M01 = double.NaN;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("\nПомилка валідації: " + ex.Message);
        }

        Console.WriteLine("\nОдинична матриця:\n" + Matrix2x2.Identity);

        Console.WriteLine("\nb[1,0] = " + b[1, 0]);
        b[1, 0] = 100;
        Console.WriteLine("Після b[1,0] = 100:\n" + b);

        Console.WriteLine("\na + b:\n" + (a + b));

        Console.WriteLine("\na * 2:\n" + (a * 2));
        Console.WriteLine("\n3 * c:\n" + (3 * c));

        Console.WriteLine("\na == c: " + (a == c));
        Console.WriteLine("a != c: " + (a != c));
        Console.WriteLine("c.Equals(new Matrix2x2(1,2,3,4)): " + c.Equals(new Matrix2x2(1, 2, 3, 4)));
        Console.WriteLine("HashCode c: " + c.GetHashCode());
    }
}
