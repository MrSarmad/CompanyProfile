using ASI.Services.Access;
using ASI.Services.Access.WebApi.Startup;
using ASI.Services.Http.Security;
using ASI.Services.WebApi;
using ASI.Services.WebApi.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Algolia.Startup;
using CompanyProfile.Core.Context;
using CompanyProfile.Core.Startup;
using CompanyProfile.Data.Startup;
using CompanyProfile.Web.Security;
using Newtonsoft.Json;
using System;

namespace CompanyProfile.Web.Api
{
    public static class DependencyInjection
    {
        /// <summary>
        /// CompanyProfile registrations. This is where to register all objects needed by your code
        /// </summary>
        public static void AddWebApi(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddLogging();

            RegisterUser(services);

            services.AddCore();
            services.AddData();
            services.AddAlgolia();
        }

        /// <summary>
        /// Register all things pertaining to the current user
        /// </summary>
        private static void RegisterUser(IServiceCollection services)
        {

            //register a service which has the current user information
            services.AddScoped<IUserInformation, WebUserInformation>();

            //if a tenant-based application, register a service that gives the current tenantId
            services.AddScoped<ITenantContext, WebTenantContext>();

            //if this uses the ASI.Services.Access library for filtering, you need to register
            // an ICurrentUser to get the current user, tenant, and any teams they're on
            services.AddScoped<ICurrentUser, AccessCurrentUser>();

            //ASI.Services.Access.WebApi allows you to filter out users' access to
            //certain endpoints based on their configured permissions
            //services.AddUserPermissionFilters();
        }
    }
}
