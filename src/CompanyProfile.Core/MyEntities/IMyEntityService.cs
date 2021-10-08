using CompanyProfile.Core.Models;
using System;
using System.Threading.Tasks;

namespace CompanyProfile.Core.MyEntities
{
    public interface IMyEntityService
    {
        Task<MyEntity> AddAsync(MyEntity toAdd);
        MyEntity? Get(long id);
        Task<MyEntity> UpdateAsync(long id, Action<MyEntity> update);
    }
}