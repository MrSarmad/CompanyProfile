using ASI.Services.Search.Models;
using System;

namespace CompanyProfile.Core.Search.Models
{
    public sealed class SearchMyEntity : ISearchModel
    {
        //Algolia ObjectID, required for all algolia fields
        public string ObjectID => Id.ToString();
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; } = null!;
        public long TenantId { get; set; }
        public long OwnerId { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
