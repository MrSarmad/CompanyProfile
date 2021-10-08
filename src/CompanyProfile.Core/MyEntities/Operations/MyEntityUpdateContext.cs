using ASI.Services.Search.Indexing;
using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using System;

namespace CompanyProfile.Core.MyEntities.Operations
{
    public sealed class MyEntityUpdateContext
    {
        public long Id { get; }
        public Action<MyEntity> UpdateFunc { get; }
        public IMyEntityLoader Loader { get; }
        public IDataTransaction DataTransaction { get; }
        public ISearchTransaction SearchTransaction { get; }
        public MyEntity? Entity { get; set; }

        public MyEntityUpdateContext(
            long id,
            Action<MyEntity> updateFunc,
            IMyEntityLoader loader,
            IDataTransaction dataTransaction,
            ISearchTransaction searchTransaction
            )
        {
            Id = id;
            UpdateFunc = updateFunc ?? throw new ArgumentNullException(nameof(updateFunc));
            Loader = loader ?? throw new ArgumentNullException(nameof(loader));
            DataTransaction = dataTransaction ?? throw new ArgumentNullException(nameof(dataTransaction));
            SearchTransaction = searchTransaction ?? throw new ArgumentNullException(nameof(searchTransaction));
        }        
    }
}
