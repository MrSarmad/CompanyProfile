using System.Net;
using ASI.Services.Http;
using ASI.Services.Http.Logging;
using ASI.Services.WebApi.Middleware;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CompanyProfile.Web.Api.Infrastructure;
using WebApiTemplate.Core.Infrastructure;

namespace CompanyProfile.Web.Api
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;

        static Startup()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add all standard ASI registrations
            services.AddAsiWebApi(Configuration, _environment);

            //add ASI Authentication
            services.AddAsiAuth(Configuration);

            //add Microsoft-specific standards
            services.AddMsApi();

            //Add all registrations for this application
            services.AddWebApi();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Additional services can be injected into this method.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // seems to cause issues in local and integration tests
                app.UseHttpsRedirection(); //redirects HTTP requests to HTTPS.

                // seems to cause issues in local, so only use on non-dev
                // Insert Security headers recommended by OWASP
                app.ConfigureSecureHeaders(Configuration);

                //enable exception handling
                app.ConfigureExceptionHandler(Configuration);

                // Enable Throttling
                app.UseMiddleware<CustomIpRateLimitMiddleware>();

                //// HSTS policy is commented as the HSTS control is now moved to Security header configuration
                //Set HSTS policy to force Strict Transport Security Protocol in non Dev environments
                //app.UseHsts(hsts => hsts.MaxAge(365));         
            }

            app.ConfigureLogging(Configuration, loggerFactory);

            app.UseStaticFiles(); //returns static files and short-circuits further request processing.
            //app.UseCookiePolicy();

            // Enable Swagger
            app.ConfigureSwagger();

            app.UseRouting();

            // Enable CORS, provide policy if need to apply on all controllers, 
            // otherwise enable on individual controller with specific policy
            app.UseCors();

            //app.UseRequestLocalization(); //middleware for localizing into different languages and cultures
            //app.UseResponseCaching();

            app.UseAuthentication(); //attempts to authenticate the user before they're allowed access to secure resources.

            app.UseAuthorization(); // authorizes a user to access secure resources.

            // Enable Response Compression
            app.UseResponseCompression();  //supports gzip and br compression out of the box

            // Insert CorrelationId
            app.UseCorrelationIdHandler();

            app.UseRequestResponseLogging(Configuration);  // This is ASI shared Request-Response logger utility
            // app.UseRequestResponseLoggingMiddleware(); // This is a local logger middleware for request response logging, useful during development

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                //if using reverseProxySettings.json, enable this
                //endpoints.MapReverseProxy();
            });

            //ensure that the automapper config is valid before letting the app run.
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
