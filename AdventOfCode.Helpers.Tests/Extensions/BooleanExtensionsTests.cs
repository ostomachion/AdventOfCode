using AdventOfCode.Helpers.Extensions;

namespace AdventOfCode.Helpers.Tests.Extensions;

public class BooleanExtensionsTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Set(bool value)
    {
        value.Set();
        Assert.True(value);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Unset(bool value)
    {
        value.Unset();
        Assert.False(value);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Toggle(bool value)
    {
        var original = value;
        value.Toggle();
        Assert.Equal(!original, value);
    }
}
