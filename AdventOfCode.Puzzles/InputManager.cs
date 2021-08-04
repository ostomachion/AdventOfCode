using System;
using System.IO;

namespace AdventOfCode.Puzzles
{
    public class InputManager
    {
        public readonly Client client;

        public InputManager(Client client)
        {
            this.client = client;
        }

        public string Get(int year, int day)
        {
            string path = Paths.GetInputPath(year, day);
            if (!File.Exists(path))
                File.WriteAllText(path, this.client.GetInput(year, day));

            return File.ReadAllText(path);
        }
    }
}