using ASI.Services.Search.Indexing;
using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.MyEntities.Operations;
using System;
using System.Threading.Tasks;

namespace CompanyProfile.Core.MyEntities
{
    public sealed class MyEntityService : IMyEntityService
    {
        private readonly IDataAccess _dataAccess;
        private readonly IMyEntityLoader _myEntityLoader;
        private readonly ISearchTransactionProvider _searchTransactionProvider;

        public MyEntityService(IDataAccess dataAccess, IMyEntityLoader myEntityLoader, ISearchTransactionProvider searchTransactionProvider)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
            _myEntityLoader = myEntityLoader ?? throw new ArgumentNullException(nameof(myEntityLoader));
            _searchTransactionProvider = searchTransactionProvider ?? throw new ArgumentNullException(nameof(searchTransactionProvider));
        }

        public MyEntity? Get(long id)
        {
            return _myEntityLoader.Get(id);
        }

        /// <summary>
        /// Simple scenario that doesn't really need unit testing
        /// </summary>
        public async Task<MyEntity> AddAsync(MyEntity toAdd)
        {
            var trans = _dataAccess.CreateTransaction();
            var searchTrans = _searchTransactionProvider.CreateTransaction();

            trans.Add(toAdd);
            searchTrans.Add(toAdd);

            await trans.CommitAsync();
            await searchTrans.CommitAsync();

            return toAdd;
        }

        /// <summary>
        /// More compex scenario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<MyEntity> UpdateAsync(long id, Action<MyEntity> update)
        {
            var trans = _dataAccess.CreateTransaction();
            var searchTrans = _searchTransactionProvider.CreateTransaction();
            var context = new MyEntityUpdateContext(id, update, _myEntityLoader, trans, searchTrans);
            var op = new MyEntityUpdateOperation(context);

            op.Load();
            op.StageChanges();

            await trans.CommitAsync();
            searchTrans.Commit();

            //commented out for app template example
            //if we handle the not found case, we can assert that context.Entity will have a value here.
            return context.Entity!;
        }
    }
}
