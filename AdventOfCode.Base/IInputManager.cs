using System;
using System.IO;

namespace AdventOfCode.Base
{
    public interface IInputManager
    {
        string GetPath(int year, int day);
        string Get(int year, int day);
    }
}