using Microsoft.AspNetCore.WebUtilities;
using System.Text.RegularExpressions;
using System.Web;

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
        return task.Result.ReplaceLineEndings("\n");
    }

    public string GetPuzzle(int year, int day)
    {
        return DownloadString($"{BaseUrl}/{year}/day/{day}");
    }

    public string GetInput(int year, int day)
    {
        return DownloadString($"{BaseUrl}/{year}/day/{day}/input").TrimEnd('\n');
    }

    public string SubmitAnswer(int year, int day, int part, string answer)
    {
        var url = $"{BaseUrl}/{year}/day/{day}/answer";
        var query = new Dictionary<string, string>()
        {
            ["level"] = part.ToString(),
            ["answer"] = answer
        };

        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(url),
            Content = new FormUrlEncodedContent(query),
            Method = HttpMethod.Post,
        };
        request.Headers.Add("Cookie", $"session={session}");
        
        var response = httpClient.SendAsync(request);
        response.Wait();
        response.Result.EnsureSuccessStatusCode();

        var task = response.Result.Content.ReadAsStringAsync();
        task.Wait();

        var responseString = task.Result;

        var path = Paths.GetResponsePath(year, day, part);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, responseString);

        var start = responseString.IndexOf("<main>") + "<main>".Length;
        var end = responseString.IndexOf("</main>");
        var message = responseString[start..end];
        message = Regex.Replace(message, "<[^>]+>", "");
        message = HttpUtility.HtmlDecode(message);
        message = message.Trim();

        return message;
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
