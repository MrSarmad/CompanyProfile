namespace CompanyProfile.Core.Context
{
    public interface ITenantContext
    {
        long TenantId { get; }
    }
}