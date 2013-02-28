namespace AsyncMaps
{
    internal class TileResult
    {
        public TileResult(string providerName)
        {
            ProviderName = providerName;
        }

        public string ProviderName { get; private set; }
    }
}