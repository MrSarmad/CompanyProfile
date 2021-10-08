using ASI.Services.Http.Exceptions;
using ASI.Services.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiTemplate.Core.Infrastructure
{
    /// <summary>
    /// Register's logging using ASI's standards
    /// </summary>
    public static class LoggingExtensions
    {
        public static void RegisterAsiExceptionLogging(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IExceptionLogger), new ExceptionLogger()));

            // register HttpLoggingPolicy Configuration
            //IConfigurationSection loggingSec = configuration.GetSection("HttpLoggingPolicy");
            //services.Configure<HttpLoggingPolicyOptions>(loggingSec);            
        }
    }
}
