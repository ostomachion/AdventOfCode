using System.Collections.Generic;
using System;
using System.Linq;

namespace Kleene
{
    public class ExpressionContext
    {
        public bool Consuming { get; set; } = true;
        public bool Producing { get; set; } = true;
        public bool IsAtEnd => Index == Input.Length;
        public string Input { get; }
        public int Index { get; private set; }
        private readonly Stack<string> outputStack = new();
        public string Output => String.Join("", outputStack.Reverse());

        public CaptureTree CaptureTree { get; } = new();
        public FunctionList FunctionList { get; } = new();

        public ExpressionContext(string input)
        {
            Input = input;
        }

        public void Consume(int length)
        {
            if (Consuming)
                Index += length;
        }

        public void Unconsume(int length)
        {
            if (Consuming)
                Index -= length;
        }

        public void Produce(string value)
        {
            if (Producing)
                outputStack.Push(value);
        }

        public void Unproduce()
        {
            if (Producing)
                outputStack.Pop();
        }
    }
}
