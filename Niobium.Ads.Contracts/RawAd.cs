namespace Niobium.Ads
{
    public class RawAd
    {
        public required string AdArchiveId { get; set; }

        public required string AdDetailUrl { get; set; }

        public string? PageId { get; set; }

        public string? PageProfileUri { get; set; }

        public string? AdvertiserName { get; set; }

        public string? Status { get; set; }

        public string? FirstSeenIso { get; set; }

        public string? LastSeenIso { get; set; }

        public string? CreativeType { get; set; }

        public string? PrimaryText { get; set; }

        public string? Headline { get; set; }

        public string? Description { get; set; }

        public string? CallToAction { get; set; }

        public List<string> MediaUrls { get; set; } = [];

        public List<string> TargetingInferences { get; set; } = [];

        public string? LandingPageUrl { get; set; }

        public required string RetrievalDateIso { get; set; }
    }
}
