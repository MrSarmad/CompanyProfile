using CompanyProfile.Core.Models;

namespace CompanyProfile.Core.Data
{
    public interface ISqlStoreCommand<T> : IStoreCommand
        where T : class, IEntity
    {

    }
}
