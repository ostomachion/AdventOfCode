using System.Net;
using System;
using System.IO;

namespace AdventOfCode.Base
{
    public class Client : IDisposable
    {
        public const string BaseUrl = "https://adventofcode.com";

        private readonly WebClient webClient = new();
        private bool disposedValue;

        public Client()
        {
            var session = File.ReadAllText(Paths.SessionPath);
            this.webClient.Headers.Add("Cookie", $"session={session}");
        }

        public string GetPuzzle(int year, int day)
        {
            return this.webClient.DownloadString($"{BaseUrl}/{year}/day/{day}");
        }

        public string GetInput(int year, int day)
        {
            return this.webClient.DownloadString($"{BaseUrl}/{year}/day/{day}/input").TrimEnd('\n');
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.webClient.Dispose();
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
}