using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CopyToAsync
{
    public static class StreamExtensions
    {
        public static async Task CopyToAsync(this Stream source, Stream destination,
                                             CancellationToken cancellationToken,
                                             IProgress<long> progress)
        {
        }
    }
}
