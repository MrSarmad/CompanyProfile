using ASI.Services.Http.Security;
using ASI.Services.Security;
using Microsoft.AspNetCore.Http;
using CompanyProfile.Core.Context;
using CompanyProfile.Data.Context;
using System;

namespace CompanyProfile.Web.Security
{
    /// <summary>
    /// Example of how you could store a tenant id. This allows you to inject the current tenant
    /// throughout the application. Security policy expects this in order to filter out
    /// every object from being accessed across-tenant.
    /// </summary>
    public sealed class WebTenantContext : ITenantContext
    {
        //this service comes from ASI.Services
        private readonly IAuthenticatedUserProvider _authUserProvider;

        public WebTenantContext(IAuthenticatedUserProvider authUserProvider)
        {
            _authUserProvider = authUserProvider;
        }

        private AuthenticatedUser? _user;
        private AuthenticatedUser User => _user ?? _authUserProvider.GetUser();


        /// <summary>
        /// TenantId of the current user (assuming this is TENT-based app)
        /// </summary>
        public long TenantId => User.ApplicationId;
    }
}
