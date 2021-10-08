using ASI.Services.Access;
using System;

namespace CompanyProfile.Core.Models
{
    //Keep this as a separate class from Entity to force use of the Query() methods when querying these types
    public abstract class TenantEntity : IEntity
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// TenantId is used for filtration of all queries of these entities in the normal ILoaders
        /// </summary>
        public long TenantId { get; set; }
        /// <summary>   
        /// Denotes that the entity is soft-deleted
        /// </summary>
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdateDate { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public abstract class ShareableTenantEntity : TenantEntity, IShareable
    {
        public long OwnerId { get; set; }
        public string Access { get; set; } = "E";

        public bool IsEditable { get; set; }
        public bool IsVisible { get; set; }

        public abstract string ResourceType { get; }
    }
}
