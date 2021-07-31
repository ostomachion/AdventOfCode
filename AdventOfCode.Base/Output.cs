namespace AdventOfCode.Base
{
    public class Output
    {
        public string Value { get; }

        public Output(string value)
        {
            Value = value;
        }

        public static implicit operator string(Output value) => value.Value;
        public static implicit operator Output(string value) => new(value);
    }
}