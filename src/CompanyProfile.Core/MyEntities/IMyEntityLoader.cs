using CompanyProfile.Core.Models;

namespace CompanyProfile.Core.MyEntities
{
    public interface IMyEntityLoader
    {
        MyEntity? Get(long id);
    }
}