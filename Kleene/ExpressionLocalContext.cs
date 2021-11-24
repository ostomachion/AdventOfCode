using System;
using System.Collections.Generic;
using System.Linq;

namespace Kleene;

public class ExpressionLocalContext
{
    public bool Consuming { get; set; } = true;
    public bool Producing { get; set; } = true;
    public bool IsAtStart => Index == 0;
    public bool IsAtEnd => Index == Input.Length;
    public string Input { get; }
    public int Index { get; set; }
    public Stack<string> OutputStack { get; } = new();
    public string Output => string.Join("", OutputStack.Reverse());

    public ExpressionLocalContext(string input)
    {
        Input = input;
    }

    public void Consume(int length)
    {
        if (Consuming)
        {
            Index += length;
        }
    }

    public void Unconsume(int length)
    {
        if (Consuming)
        {
            Index -= length;
        }
    }

    public void Produce(string value)
    {
        if (Producing)
        {
            OutputStack.Push(value);
        }
    }

    public void Unproduce()
    {
        if (Producing)
        {
            OutputStack.Pop();
        }
    }
}
