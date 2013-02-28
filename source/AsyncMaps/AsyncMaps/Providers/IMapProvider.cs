using System.Threading;
using System.Threading.Tasks;

namespace AsyncMaps.Providers
{
    internal interface IMapProvider
    {
        Task<TileResult> LoadTileAsync(Tile tile, CancellationToken cancellationToken);
    }
}