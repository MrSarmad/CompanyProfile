using CompanyProfile.Core.Exceptions;
using System;

namespace CompanyProfile.Core.MyEntities.Operations
{
    public sealed class MyEntityUpdateOperation
    {
        public MyEntityUpdateContext Context { get; }

        public MyEntityUpdateOperation(MyEntityUpdateContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public void Load()
        {
            Context.Entity = Context.Loader.Get(Context.Id).ValidateFound(Context.Id);
        }

        public void Validate()
        {
            if (Context.Entity == null)
                throw new InvalidOperationException($"You must call {nameof(Load)} before {nameof(Validate)}");

            if (Context.Entity.Name == "DEBUG")
                throw new MyEntityUpdateException(Context.Entity.Name);
        }

        public void StageChanges()
        {
            if (Context.Entity != null)
            {
                var originalName = Context.Entity.Name;
                Context.UpdateFunc(Context.Entity);
                var namechanged = Context.Entity.Name != originalName;

                if (namechanged)
                {
                    //
                }

                //here would be some more complex logic like updating child objects or what have you... 
                // things that need to be unit tested.
                Context.DataTransaction.Update(Context.Entity);
                Context.SearchTransaction.Update(Context.Entity);
            }

            //else: handle error or throw? depends on the scenario
        }
    }
}
