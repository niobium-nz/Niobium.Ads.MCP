using Microsoft.Extensions.Logging;

namespace Niobium.Ads.Analyst
{
    internal class AnalystWorkflow(
        KeywordsPlanner keywordPlanner,
        AdsDiscoverer adsDiscoverer,
        ProductInfoEnricher productInfoEnricher,
        ILogger<AnalystWorkflow> logger)
    {
        public async Task DeployAsync(CancellationToken cancellationToken)
        {
            await keywordPlanner.DeployAsync(cancellationToken);
            await productInfoEnricher.DeployAsync(cancellationToken);
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
                if (rawAds == null || rawAds.SearchResults.Count <= 0)
                {
                    logger.LogWarning("Failed to get ads for keyword {keyword}", keyword);
                    continue;
                }

                var candidates = await productInfoEnricher.RunAsync(conversationID, new ProductInfoEnricherInput { RawAds = rawAds.SearchResults }, cancellationToken);
                Console.WriteLine($"Keyword: {keyword}, Candidates Count: {candidates?.Candidates?.Count}");
                break;
            }
        }
    }
}
