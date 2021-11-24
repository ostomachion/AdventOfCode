namespace AdventOfCode.Puzzles;

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
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, client.GetInput(year, day));
        }

        return File.ReadAllText(path);
    }
}
