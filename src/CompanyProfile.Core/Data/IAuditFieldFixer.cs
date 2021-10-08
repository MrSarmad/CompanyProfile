using CompanyProfile.Core.Context;
using CompanyProfile.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyProfile.Core.Data
{
    //Needs to be named differently from EntityState, which is defined in EF core
    public enum DbEntityState
    {
        Unknown,
        Added,
        Modified,
    }

    /// <summary>
    /// Fixup data for all entities before committing to database
    /// </summary>
    public interface IAuditFieldFixer
    {
        void FixAuditFields(IEntity entity, DbEntityState state);
    }

    /// <summary>
    /// Fixup data for all entities before committing to database
    /// </summary>
    public class AuditFieldFixer : IAuditFieldFixer
    {
        private readonly IUserInformation _userInformation;

        public AuditFieldFixer(IUserInformation userInformation)
        {
            _userInformation = userInformation;
        }

        public void FixAuditFields(IEntity entity, DbEntityState state)
        {
            if (entity is ShareableTenantEntity shareable && shareable.OwnerId == 0)
            {
                shareable.OwnerId = _userInformation?.UserId ?? 0;
            }
            if (state == DbEntityState.Added)
            {
                entity.CreateDate = DateTime.UtcNow;
                entity.CreatedBy = _userInformation?.UserName ?? string.Empty;
            }
            else if (state == DbEntityState.Modified)
            {
                entity.UpdateDate = DateTime.UtcNow;
                entity.UpdatedBy = _userInformation?.UserName;
            }
        }
    }
}
