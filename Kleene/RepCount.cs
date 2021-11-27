namespace Kleene;

public class RepCount
{
    internal class Model : IModel<RepCount>
    {
        public int? Min { get; set; }
        public int? Max { get; set; }

        public RepCount Convert()
        {
            return Min is not null && Max is not null ? new(Min.Value, Max.Value)
                : Min is not null ? new(min: Min.Value)
                : Max is not null ? new(max: Max.Value)
                : new();
        }
    }

    public const int Unbounded = -1;

    public int Min { get; }
    public int Max { get; }
    public bool IsUnbounded => Max == Unbounded;

    public RepCount(int min = 0, int max = Unbounded)
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
