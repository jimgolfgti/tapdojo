using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncMaps.Providers;

namespace AsyncMaps
{
    internal class TileLoader
    {
        private readonly IEnumerable<IMapProvider> _providers;

        public TileLoader()
        {
            _providers = new IMapProvider[]
                {
                    new MapProviderOne(),
                    new MapProviderTwo(), 
                    new MapProviderThree()
                };
        }

        public async Task<TileResult> LoadTile(Tile tile)
        {
            throw new NotImplementedException();
        }
    }
}