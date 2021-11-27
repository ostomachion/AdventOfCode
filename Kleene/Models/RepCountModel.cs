namespace Kleene.Models;

public class RepCountModel : IModel<RepCount>
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
