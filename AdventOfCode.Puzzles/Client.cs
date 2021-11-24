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

    private async Task<string> DownloadStringAsync(string url)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(url),
            Method = HttpMethod.Get,
        };
        request.Headers.Add("Cookie", $"session={session}");
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetPuzzleAsync(int year, int day)
    {
        return await DownloadStringAsync($"{BaseUrl}/{year}/day/{day}");
    }

    public async Task<string> GetInputAsync(int year, int day)
    {
        return (await DownloadStringAsync($"{BaseUrl}/{year}/day/{day}/input")).TrimEnd('\n');
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
