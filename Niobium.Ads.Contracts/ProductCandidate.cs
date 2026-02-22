namespace Niobium.Ads
{
    public class ProductCandidate
    {
        public required string CandidateId { get; set; }

        public required string CandidateLabel { get; set; }

        public string? LandingPageDomain { get; set; }

        public string? LikelyProductName { get; set; }

        public string? CategoryGuess { get; set; }

        public required string ClusterConfidence { get; set; }

        public List<MetaAd> Ads { get; set; } = [];
    }
}
