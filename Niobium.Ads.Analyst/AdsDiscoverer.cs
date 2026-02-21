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
            ResponseTool.CreateWebSearchPreviewTool(
                WebSearchToolLocation.CreateApproximateLocation(country: "US")
            ),
            ResponseTool.CreateMcpTool("adslibrary", new Uri("https://niobiumadsmcpapp.mangosky-a7b92dc1.westus2.azurecontainerapps.io/"), authorizationToken: "CeYXCoG4U82EEITMpkDiTh1EOmi", toolCallApprovalPolicy: new McpToolCallApprovalPolicy(GlobalMcpToolCallApprovalPolicy.NeverRequireApproval))
        ];
    }
}
