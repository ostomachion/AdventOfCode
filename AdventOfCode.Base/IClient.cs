using System.Net;
using System;

namespace AdventOfCode.Base
{
    public interface IClient
    {
        string GetPuzzle(int year, int day);
        string GetInput(int year, int day);
    }
}