
using System;
using System.Collections.Generic;

public static class FunctionalUtils
{
    public static List<TResult> Map<T, TResult>(
        List<T> list, Func<T, TResult> mapper)
    {
        List<TResult> result = new List<TResult>();

        foreach (T item in list)
            result.Add(mapper(item));

        return result;
    }

    public static List<T> Filter<T>(
        List<T> list, Predicate<T> predicate)
    {
        List<T> result = new List<T>();

        foreach (T item in list)
        {
            if (predicate(item))
                result.Add(item);
        }

        return result;
    }

    public static TResult Reduce<T, TResult>(
        List<T> list,
        TResult initialValue,
        Func<TResult, T, TResult> accumulator)
    {
        TResult result = initialValue;

        foreach (T item in list)
            result = accumulator(result, item);

        return result;
    }

    public static void ForEach<T>(
        List<T> list, Action<T> action)
    {
        foreach (T item in list)
            action(item);
    }

    public static bool Any<T>(
        List<T> list, Predicate<T> predicate)
    {
        foreach (T item in list)
        {
            if (predicate(item))
                return true;
        }

        return false;
    }
}
