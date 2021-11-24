namespace AdventOfCode.Puzzles;

public class InputManager
{
    public readonly Client client;

    public InputManager(Client client)
    {
        this.client = client;
    }

    public async Task<string> GetAsync(int year, int day)
    {
        string path = Paths.GetInputPath(year, day);
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, await client.GetInputAsync(year, day));
        }

        return File.ReadAllText(path);
    }
}
