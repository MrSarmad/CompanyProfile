using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace WebApiTemplate.Core.Infrastructure
{
    public static class MicrosoftWebApiExtensions
    {
        /// <summary>
        /// Microsoft-specific registrations
        /// </summary>
        public static void AddMsApi(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();

            services.AddMvc()
                .AddNewtonsoftJson(o =>
                {
                    //no resolver = don't change anything = keep it PascalCase
                    o.SerializerSettings.ContractResolver = null;
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                //this seems to be necessary for swagger to use correct naming policy
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                ;
        }
    }
}
