namespace AdventOfCode.Puzzles;

public static class Paths
{
    static Paths()
    {
        Directory.CreateDirectory(BaseDir);
        Directory.CreateDirectory(InputDir);
        Directory.CreateDirectory(OutputDir);
        Directory.CreateDirectory(ResponseDir);
    }

    public static readonly string BaseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "AdventOfCode");
    public static readonly string InputDir = Path.Combine(BaseDir, "Input");
    public static readonly string OutputDir = Path.Combine(BaseDir, "Output");
    public static readonly string ResponseDir = Path.Combine(BaseDir, "Response");
    public static readonly string SessionPath = Path.Combine(BaseDir, ".session");

    public static string GetInputPath(int year, int day)
    {
        return Path.Combine(InputDir, year.ToString(), $"{day}.txt");
    }

    public static string GetOutputPath(int year, int day, int part)
    {
        return Path.Combine(OutputDir, year.ToString(), $"{day}-{part}.txt");
    }

    public static string GetResponsePath(int year, int day, int part)
    {
        return Path.Combine(OutputDir, year.ToString(), day.ToString(), part.ToString(), $"{DateTime.Now:yyyyMMddTHHmmss}.html");
    }
}
