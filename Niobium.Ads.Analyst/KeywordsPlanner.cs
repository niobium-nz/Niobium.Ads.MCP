using Azure.AI.Projects;
using Microsoft.Extensions.Logging;
using OpenAI.Responses;

namespace Niobium.Ads.Analyst
{
    internal class KeywordsPlanner(AIProjectClient client, ILogger<KeywordsPlanner> logger) : HostedAgent<KeywordsPlannerInput, KeywordsPlannerOutput>(client, logger)
    {
        protected override string Name => nameof(KeywordsPlanner);

        protected override IEnumerable<ResponseTool> Tools =>
        [
            ResponseTool.CreateWebSearchPreviewTool(
                WebSearchToolLocation.CreateApproximateLocation(country: "US")
            ),
        ];
    }
}
