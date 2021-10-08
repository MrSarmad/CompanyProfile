using ASI.Services.Access;
using CompanyProfile.Core.Context;
using System.Collections.Generic;

namespace CompanyProfile.Web.Security
{
    /// <summary>
    /// Serviecs that gives current user for purposes of ASI.Services.Access library for
    /// security filtering on tenantid and ownerid
    ///
    /// Could be consolidated with IUserInformation or ITenantContext
    /// </summary>
    public class AccessCurrentUser : ICurrentUser
    {
        //this service comes from ASI.Services
        private readonly IUserInformation _userInfo;
        private readonly ITenantContext _tenantContext;

        public AccessCurrentUser(IUserInformation userInfo, ITenantContext tenantContext)
        {
            _userInfo = userInfo;
            _tenantContext = tenantContext;

            //set from current user settings
            //OwnerId = _userInfo.UserId;
            //TenantId = _tenantContext.TenantId;
        }

        //could be retrieved from the ICurrentUser or the AuthenticatedUser, or loaded
        // out of a databases
        public IReadOnlyList<long> Teams => new List<long>();

        public long TenantId { get; set; }
        public long OwnerId { get; set; }
    }
}
