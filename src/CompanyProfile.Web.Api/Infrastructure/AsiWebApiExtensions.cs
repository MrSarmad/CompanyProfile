using ASI.Services.DependencyInjection;
using ASI.Services.Http.Security;
//using ASI.Services.ServiceRegistration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CompanyProfile.Web.Api;
using CompanyProfile.Web.Api.Infrastructure;

namespace WebApiTemplate.Core.Infrastructure
{
    public static class AsiWebApiExtensions
    {
        /// <summary>
        /// Standards for all ASI Web applications
        /// </summary>
        public static void AddAsiWebApi(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddOptions();
            services.AddResponseCompression();

            //defined in Infrastructure folder
            services.RegisterSwagger();
            services.RegisterAsiExceptionLogging();

            // add IServiceRegistration implementations in ASI libraries
            services.AddServicesInAssembly(configuration, environment);

            // Add Controllers
            // Set formatters for API controllers. 
            // Suppress String, XML, and HttpNoContent outputs
            services.AddControllers(options =>
            {
                // requires using Microsoft.AspNetCore.Mvc.Formatters;
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                options.OutputFormatters.RemoveType<XmlSerializerOutputFormatter>();
                //uncomment below line if like to use a generic handler for all exceptions instead of the ASI ExceptionHandler driven by app settings
                //options.Filters.Add<UnhandledExceptionFilterAttribute>(); 
            }).AddControllersAsServices();

            // Add Versioning support for APIs
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                //o.ApiVersionSelector = new CurrentImplementationApiVersionSelector(o);
            });
        }

        /// <summary>
        /// Standards for using ASI's Authentication strategies
        /// </summary>
        public static void AddAsiAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAuthentication(configuration);
            services.AddAuthorization();
        }
    }
}
