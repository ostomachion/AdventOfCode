namespace Kleene;

public class RepCount
{
    internal class Model : IModel<RepCount>
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        public RepCount Convert()
        {
            if (Min is null || Max is null)
                throw new InvalidOperationException();

            return new(Min.Value, Max.Value);
        }
    }

    public const int Unbounded = -1;

    public int Min { get; }
    public int Max { get; }
    public bool IsUnbounded => Max == Unbounded;

    public RepCount(int min, int max)
    {
        if (min < 0)
        {
            throw new ArgumentException("Min must be non-negative.", nameof(min));
        }

        if (max != -1 && max < min)
        {
            throw new ArgumentException("Max must not be smaller than min.", nameof(max));
        }

        Min = min;
        Max = max;
    }
}
