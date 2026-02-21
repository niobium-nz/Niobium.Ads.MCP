using Azure.AI.Projects;
using Microsoft.Extensions.Logging;
using OpenAI.Responses;

namespace Niobium.Ads.Analyst
{
    internal class AdsDiscoverer(AIProjectClient client, ILogger<AdsDiscoverer> logger) : HostedAgent<AdsDiscovererInput, AdsDiscovererOutput>(client, logger)
    {
        protected override string Name => nameof(AdsDiscoverer);

        protected override IEnumerable<ResponseTool> Tools =>
        [
            McpTools.AdsLibraryMcpTool,
            ResponseTool.CreateWebSearchPreviewTool(
                WebSearchToolLocation.CreateApproximateLocation(country: "US")
            ),
        ];
    }
}
