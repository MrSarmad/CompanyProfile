using ASI.Services.Http.Security;
using ASI.Services.Security;
using Microsoft.AspNetCore.Http;
using CompanyProfile.Core.Context;
using System;
using System.Linq;

namespace CompanyProfile.Web.Security
{
    /// <summary>
    /// Example of how you could retrieve the current user information
    /// IUserInformation can be modified to store all info you need about
    /// the current user and injected into services
    /// </summary>
    public class WebUserInformation : IUserInformation
    {
        //this service comes from ASI.Services
        private readonly IAuthenticatedUserProvider _authUserProvider;

        public WebUserInformation(IAuthenticatedUserProvider authUserProvider)
        {
            _authUserProvider = authUserProvider;
        }

        private AuthenticatedUser? _user;
        private AuthenticatedUser User => _user ?? _authUserProvider.GetUser();

        /// <summary>
        /// Username of the user authenticated via ASI Authentication
        /// </summary>
        public string UserName => User.Name;
        public long UserId => User.UserId;
    }
}
