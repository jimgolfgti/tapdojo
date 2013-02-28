using System;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncMaps
{
    class Program
    {
        static void Main()
        {
            RunTestAsync()
                .Wait();
        }

        private static async Task RunTestAsync()
        {
            var loader = new TileLoader();
            foreach (var sample in Tile.GetSampleTiles().Select((tile, index) => new {tile, index}))
            {
                var result = await loader.LoadTile(sample.tile).ConfigureAwait(false);

                Console.WriteLine("{0:000} Tile '{1}' loaded from provider '{2}'",
                                  sample.index,
                                  sample.tile.Label,
                                  result.ProviderName);
            }
        }
    }
}
