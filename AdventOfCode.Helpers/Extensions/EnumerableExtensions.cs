using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Helpers.Extensions;

public static class EnumerableExtensions
{
    public static Dictionary<int, TValue> ToDictionary<TValue>(this IEnumerable<TValue> enumerable)
    {
        return enumerable.Select((x, i) => (x, i)).ToDictionary(p => p.i, p => p.x);
    }

    public static long Product(this IEnumerable<int> n) => n.Aggregate(1L, (x, y) => x * y);
    public static long Product(this IEnumerable<long> n) => n.Aggregate(1L, (x, y) => x * y);
    public static BigInteger BigProduct(this IEnumerable<long> n) => n.Aggregate(BigInteger.One, (x, y) => x * y);
    public static BigInteger BigProduct(this IEnumerable<BigInteger> n) => n.Aggregate(BigInteger.One, (x, y) => x * y);


    public static long Product<T>(this IEnumerable<T> n, Func<T, int> f) => n.Aggregate(1L, (x, y) => x * f(y));
    public static long Product<T>(this IEnumerable<T> n, Func<T, long> f) => n.Aggregate(1L, (x, y) => x * f(y));
    public static BigInteger BigProduct<T>(this IEnumerable<T> n, Func<T, long> f) => n.Aggregate(BigInteger.One, (x, y) => x * f(y));
    public static BigInteger BigProduct<T>(this IEnumerable<T> n, Func<T, BigInteger> f) => n.Aggregate(BigInteger.One, (x, y) => x * f(y));

    public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> n)
    {
        if (!n.Any())
        {
            yield return Enumerable.Empty<T>();
            yield break;
        }

        foreach (var (item, i) in n.Select((item, i) => (item, i)))
        {
            foreach (var p in n.Take(i).Concat(n.Skip(i + 1)).Permutations())
            {
                yield return new[] { item }.Concat(p);
            }
        }
    }

    public static IEnumerable<T[]> Windows<T>(this IEnumerable<T> n, int window)
    {
        if (n.Count() < window)
            throw new ArgumentException("Collection is too small for window.", nameof(window));
        Queue<T> queue = new Queue<T>();
        foreach (var item in n)
        {
            queue.Enqueue(item);
            if (queue.Count == window)
            {
                yield return queue.ToArray();
                queue.Dequeue();
            }
        }
    }

    public static int? IndexOf<T>(this IEnumerable<T> n, T value)
    {
        foreach (var (item, i) in n.Select((item, i) => (item, i)))
        {
            if (item.Equals(value))
                return i;
        }
        return null;
    }

    public static IEnumerable<T> Mode<T>(this IEnumerable<T> n) where T : notnull
    {
        var count = new DefaultDictionary<T, int>();
        foreach (var item in n)
        {
            count[item]++;
        }
        var max = count.Values.Max();
        return count.Keys.Where(x => count[x] == max);
    }

    public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> n)
    {
        if (!n.Any() || !n.First().Any())
            throw new ArgumentException("Sequence contains no elements.", nameof(n));

        var length = n.First().Count();
        if (n.Any(x => x.Count() != length))
            throw new ArgumentException("Sequence is not rectangular.", nameof(n));

        List<List<T>> value = new();
        for (var i = 0; i < length; i++)
        {
            value.Add(new());
        }

        foreach (var row in n)
        {
            foreach (var (item, i) in row.Select((item, i) => (item, i)))
            {
                value[i].Add(item);
            }
        }

        return value;
    }
}
