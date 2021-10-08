using System;

namespace CompanyProfile.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static T GetService<T>(this IServiceProvider sp)
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetService<T>(sp)!;
        }
    }
}
