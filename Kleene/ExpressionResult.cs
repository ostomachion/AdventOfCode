namespace Kleene
{
    public class ExpressionResult
    {
        public string Input { get; }
        public string Output { get; }


        public ExpressionResult() : this("") { }
        
        public ExpressionResult(string value) : this(value, value) { }

        public ExpressionResult(string input, string output)
        {
            Input = input;
            Output = output;
        }
    }
}
