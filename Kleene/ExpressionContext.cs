using System;
namespace Kleene
{
    public class ExpressionContext
    {
        public ExpressionLocalContext Local { get; set; }
        public CaptureTree CaptureTree { get; } = new();
        public FunctionList FunctionList { get; } = new();

        public bool Ratchet { get; set; }

        public ExpressionContext(string input)
        {
            Local = new(input);
        }

        public void Consume(int length) => Local.Consume(length);

        public void Unconsume(int length) => Local.Unconsume(length);

        public void Produce(string value) => Local.Produce(value);

        public void Unproduce() => Local.Unproduce();
    }
}
