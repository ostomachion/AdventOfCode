using AdventOfCode.Helpers.Cartesian.Boxes;
using AdventOfCode.Helpers.DataStructures;

namespace AdventOfCode.Helpers.Cartesian.Grids;

public record SparsePlaneGrid2D<T> : Plane2D, IGrid<Coordinate2D, T>
    where T : notnull
{
    public static new SparsePlaneGrid2D<T> Infinite => new(Interval.Infinite, Interval.Infinite);

    private readonly DefaultDictionary<Coordinate2D, T> values = new();

    public IEnumerable<Coordinate2D> Keys => values.Keys;
    public IEnumerable<T> Values => values.Values;

    public Func<Coordinate2D, IEnumerable<KeyValuePair<Coordinate2D, long>>> Metric { get; set; }

    public T? this[Coordinate2D c]
    {
        get => Contains(c) ? values[Loop(c)] : throw new IndexOutOfRangeException();
        set => values[Loop(c)] = Contains(c) ? value : throw new IndexOutOfRangeException();
    }

    public T? this[long x, long y]
    {
        get => this[new Coordinate2D(x, y)];
        set => this[new Coordinate2D(x, y)] = value;
    }

    public SparsePlaneGrid2D(Interval i, Interval j)
        : base(i, j)
    {
        Metric = OrthogonalMetric;
    }

    public IEnumerator<KeyValuePair<Coordinate2D, T>> GetEnumerator() => values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<Coordinate2D> OrthogonalNeighbors(Coordinate2D coordinate)
    {
        return new[]
        {
            Loop(coordinate + Coordinate2D.I),
            Loop(coordinate - Coordinate2D.I),
            Loop(coordinate + Coordinate2D.J),
            Loop(coordinate - Coordinate2D.J)
        }.Where(Contains);
    }

    public IEnumerable<KeyValuePair<Coordinate2D, long>> OrthogonalMetric(Coordinate2D coordinate)
    {
        return OrthogonalNeighbors(coordinate).Select(x => new KeyValuePair<Coordinate2D, long>(x, 1));
    }

    public long Distance(Coordinate2D start, Coordinate2D end)
    {
        HashSet<Coordinate2D> unvisited = new(Keys);
        Dictionary<Coordinate2D, long> distances = new();
        foreach (var c in Keys)
            distances.Add(c, long.MaxValue);
        distances[start] = 0;

        var current = start;
        while (true)
        {
            foreach (var (n, d) in Metric(current).Where(x => unvisited.Contains(x.Key)))
            {
                var nd = distances[current] == long.MaxValue ? long.MaxValue : distances[current] + d;
                if (nd < distances[n])
                    distances[n] = nd;
            }
            unvisited.Remove(current);

            if (current == end)
                return distances[end];

            if (distances.Values.Min() == long.MaxValue)
                throw new Exception("No path.");

            current = unvisited.MinBy(x => distances[x])!;
        }
    }
}
