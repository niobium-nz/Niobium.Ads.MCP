using Azure.AI.Projects;
using Microsoft.Extensions.Logging;
using OpenAI.Responses;

namespace Niobium.Ads.Analyst
{
    internal class VendorProfiler(AIProjectClient client, ILogger<VendorProfiler> logger) : HostedAIAgent(client, logger)
    {
        public override string Name => nameof(VendorProfiler);

        protected override IEnumerable<ResponseTool> Tools =>
        [
            ResponseTool.CreateWebSearchPreviewTool(
                WebSearchToolLocation.CreateApproximateLocation(country: "NZ")
            ),
        ];
    }
}
