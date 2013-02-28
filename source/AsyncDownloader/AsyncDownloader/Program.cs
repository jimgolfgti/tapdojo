using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncDownloader
{
    class Program
    {
        static void Main()
        {
        }

        private static IEnumerable<string> GetUrlList()
        {
            return Enumerable
                .Range(1, 800)
                .Select(i => string.Format("/file{0:000}", i))
                .ToArray();
        }
    }

    internal class Resource
    {
        private static readonly Random Generator = new Random(DateTime.Now.Second);
        private readonly string _url;

        public Resource(string url)
        {
            _url = url;
        }

        public Task DownloadAsync()
        {
            var delay = Generator.Next(400, 3500);

            return Task
                .Delay(delay)
                .ContinueWith(_ => Console.WriteLine("Downloaded {0} in {1} ms", _url, delay));
        }
    }
}
