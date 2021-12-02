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
}
