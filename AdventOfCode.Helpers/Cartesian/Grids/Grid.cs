namespace AdventOfCode.Helpers.Cartesian.Grids
{
    public static class Grid
    {
        public static SparseLineGrid1D<T> MakeSparse<T>(Interval x) where T : notnull => new(x);
        public static SparsePlaneGrid2D<T> MakeSparse<T>(Interval x, Interval y) where T : notnull => new(x, y);
        public static SparseSpaceGrid3D<T> MakeSparse<T>(Interval x, Interval y, Interval z) where T : notnull => new(x, y, z);

        public static SparseLineGrid1D<T> Infinite1D<T>() where T : notnull => SparseLineGrid1D<T>.Infinite;
        public static SparseLineGrid2D<T> Infinite2D<T>() where T : notnull => SparseLineGrid2D<T>.Infinite;
        public static SparseLineGrid3D<T> Infinite3D<T>() where T : notnull => SparseLineGrid3D<T>.Infinite;
    }
}