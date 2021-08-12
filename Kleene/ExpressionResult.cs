namespace Kleene
{
    public class ExpressionResult
    {
        public string Input { get; }
        public string Output { get; }

        public ExpressionResult(string value)
        {
            Input = value;
            Output = value;
        }

        public ExpressionResult(string input, string output)
        {
            Input = input;
            Output = output;
        }
    }
}
