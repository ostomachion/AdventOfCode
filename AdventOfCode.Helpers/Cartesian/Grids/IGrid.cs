namespace AdventOfCode.Helpers.Cartesian.Grids
{
    public interface IGrid<TCoordinate, T> : IEnumerable<KeyValuePair<TCoordinate, T>>
        where TCoordinate : ICoordinate
        where T : notnull
    {

    }
}