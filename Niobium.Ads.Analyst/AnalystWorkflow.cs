using Microsoft.Extensions.Logging;

namespace Niobium.Ads.Analyst
{
    internal class AnalystWorkflow(
        KeywordsPlanner keywordPlanner,
        AdsDiscoverer adsDiscoverer,
        ILogger<AnalystWorkflow> logger)
    {
        public async Task DeployAsync(CancellationToken cancellationToken)
        {
            await keywordPlanner.DeployAsync(cancellationToken);
            await adsDiscoverer.DeployAsync(cancellationToken);
        }

        public async Task RunAsync(string conversationID, CancellationToken cancellationToken)
        {
            await DeployAsync(cancellationToken);

            KeywordsPlannerInput input = new()
            {
                 CategoryFocus = "Dog Toy",
                 Country = "US",
            };
            var keywords = await keywordPlanner.RunAsync(conversationID, input, cancellationToken);
            if (keywords == null || keywords.OptimizedKeywords.Count <= 0)
            {
                logger.LogError("Failed to get keywords for focus {focus}", input.CategoryFocus);
                return;
            }

            foreach (var keyword in keywords.OptimizedKeywords)
            {
                var discovererInput = new AdsDiscovererInput
                {
                    Keyword = keyword,
                    Country = input.Country,
                };
                var rawAds = await adsDiscoverer.RunAsync(conversationID, discovererInput, cancellationToken);
                Console.WriteLine(rawAds);
                break;
            }
        }
    }
}
