using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncMaps.Providers
{
    internal abstract class MapProvider : IMapProvider
    {
        private static readonly Random Generator = new Random(DateTime.Now.Second);
        private readonly string _name;

        protected MapProvider(string name)
        {
            _name = name;
        }

        public Task<TileResult> LoadTileAsync(Tile tile, CancellationToken cancellationToken)
        {
            var random = Generator.Next(1, 10);
            if (random%6 == 0)
                return Task
                    .Delay(500)
                    .ContinueWith<TileResult>(task => { throw new Exception(); });
            
            return Task
                .Delay(random*100)
                .ContinueWith(_ =>
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        return new TileResult(_name);
                    });
        }
    }
}