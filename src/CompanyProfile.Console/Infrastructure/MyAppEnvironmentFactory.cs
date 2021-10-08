using System.Collections.Generic;
using ASI.Console;

namespace CompanyProfile.Console
{
    public class CompanyProfileEnvironmentFactory : IEnvironmentFactory<CompanyProfileEnvironment>
    {
        public IReadOnlyCollection<string> ValidEnvironments => new[] { "DEV", "UAT", "PROD" };

        public CompanyProfileEnvironment Instance(string arg)
        {
            return new CompanyProfileEnvironment(arg);
        }
    }
}
