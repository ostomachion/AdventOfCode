namespace AdventOfCode.Puzzles;

public class Client : IDisposable
{
    public const string BaseUrl = "https://adventofcode.com";

    private readonly HttpClient httpClient = new();
    private bool disposedValue;

    private readonly string session;

    public Client()
    {
        session = File.ReadAllText(Paths.SessionPath);
    }

    private string DownloadString(string url)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
        };
        request.Headers.Add("Cookie", $"session={session}");

        var response = httpClient.SendAsync(request);
        response.Wait();
        response.Result.EnsureSuccessStatusCode();

        var task = response.Result.Content.ReadAsStringAsync();
        task.Wait();
        return task.Result;
    }

    public string GetPuzzle(int year, int day)
    {
        return DownloadString($"{BaseUrl}/{year}/day/{day}");
    }

    public string GetInput(int year, int day)
    {
        return DownloadString($"{BaseUrl}/{year}/day/{day}/input").TrimEnd('\n');
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                httpClient.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
