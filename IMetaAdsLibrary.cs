namespace AdsTransparency
{
    public interface IMetaAdsLibrary
    {
        Task<MetaAdsSearchResponse> SearchAdsAsync(string keyword, Country country, DateOnly? activeSince = null);
    }
}
        