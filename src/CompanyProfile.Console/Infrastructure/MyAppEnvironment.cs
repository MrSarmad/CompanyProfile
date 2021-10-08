using ASI.Services.Access;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.Context;
using CompanyProfile.Core.Startup;
using CompanyProfile.Data.Startup;
using System;
using System.Collections.Generic;

namespace CompanyProfile.Console
{
    public class CompanyProfileEnvironment
    {
        public CompanyProfileEnvironment(string env)
        {
            Env = env;
        }

        public long TenantId { get; set; }
        public string Env { get; }
    }

    public class ConsoleTenantContext : ITenantContext
    {
        public ConsoleTenantContext(long tenantId)
        {
            TenantId = tenantId;
        }

        public long TenantId { get; }
    }

    public class ConsoleUserInformation : IUserInformation, ICurrentUser
    {
        private readonly long _userId;

        public ConsoleUserInformation(long userId)
        {
            _userId = userId;
        }

        public string UserName => $"CompanyProfile Console - {_userId}";

        public IReadOnlyList<long> Teams => new List<long>();

        public long TenantId { get; set; }
        public long OwnerId { get; set; }

        public long UserId => _userId;
    }
}
