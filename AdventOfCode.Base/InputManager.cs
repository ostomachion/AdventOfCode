using System;
using System.IO;

namespace AdventOfCode.Base
{
    public class InputManager : IInputManager
    {
        public static readonly string OutputDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "AdventOfCode", "Input");

        private readonly IClient client;

        public InputManager(IClient client)
        {
            this.client = client;
        }

        public string GetPath(int year, int day)
        {
            var dir = Path.Combine(OutputDir, year.ToString());
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, day.ToString() + ".txt");
        }

        public string Get(int year, int day)
        {
            string path = GetPath(year, day);
            if (!File.Exists(path))
                File.WriteAllText(path, this.client.GetInput(year, day));

            return File.ReadAllText(path);
        }
    }
}