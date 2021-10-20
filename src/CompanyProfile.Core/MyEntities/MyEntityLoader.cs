using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using CompanyProfile.Personify;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProfile.Core.MyEntities
{
    //In orders code, the implementation of this loader had to be in the Data project
    //however, with the way we've abstracted our IDataAccess, it's not necessary to use
    // EF-specific code in the loaders.
    // Of course, it's always an option to put the loader into Data project,
    // but in many cases even the loaders should strive to not be dependent on EF-specific functions
    public sealed class MyEntityLoader : IMyEntityLoader
    {
        private readonly IDataAccess _dataAccess;
        private readonly IApiAccess _apiAccess;

        public MyEntityLoader(IDataAccess dataAccess, IApiAccess apiAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _apiAccess = apiAccess ?? throw new ArgumentNullException(nameof(apiAccess));
        }

        public MyEntity? Get(long id)
        {
            //return _dataAccess.QueryTenant<MyEntity>()
            //    .Where(x => x.Id == id)
            //    .FirstOrDefault();

            var response = string.Empty; // _apiAccess.GetBasicInfo().Result;

            return new MyEntity {
                Description = response
            };
            
        }
    }
}
