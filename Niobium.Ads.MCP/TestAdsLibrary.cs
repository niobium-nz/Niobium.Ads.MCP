using System.Text.Json;

namespace Niobium.Ads.MCP
{
    internal class TestAdsLibrary : IMetaAdsLibrary
    {
        private const string TestDataSource = "Niobium.Ads.MCP.dog-hair-removal.json";

        public async Task<MetaAdsSearchResponse> SearchAdsAsync(string keyword, Country country, DateOnly? activeSince = null)
        {
            using var stream = this.GetType().Assembly.GetManifestResourceStream(TestDataSource)!;
            using var reader = new StreamReader(stream);
            var result = await JsonSerializer.DeserializeAsync<MetaAdsSearchResponse>(stream, SerializationOptions.SnakeCase);
            return result!;
        }
    }
}
