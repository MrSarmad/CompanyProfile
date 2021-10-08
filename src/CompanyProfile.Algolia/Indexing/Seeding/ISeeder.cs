using CompanyProfile.Core.Models;

namespace CompanyProfile.Algolia.Seeding
{
    public interface ISeeder
    {
        IndexType Type { get; }
        void Seed(AlgoliaSeedOptions options);
        int PageSize { get; }
    }

    public interface ISeeder<T> : ISeeder
        where T : class, IEntity
    {
    }
}
