using System;

namespace CompanyProfile.Core.Models
{
    public interface IEntity
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// Denotes that the entity is soft-deleted
        /// </summary>
        bool IsDeleted { get; set; }
        DateTime CreateDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdateDate { get; set; }
        string? UpdatedBy { get; set; }
    }
}
