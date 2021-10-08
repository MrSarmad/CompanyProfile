namespace CompanyProfile.Core.Search.Models
{
    /// <summary>
    /// Denotes how the search term should be transformed when sending to the search service
    /// </summary>
    public enum TermTransformation
    {
        Match,
        Contains
    }
}
