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
}
