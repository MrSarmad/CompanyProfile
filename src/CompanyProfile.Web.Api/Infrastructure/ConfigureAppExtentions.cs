using ASI.Services.Http.Exceptions;
using ASI.Services.Logging;
using ASI.Services.WebApi;
using ASI.Services.WebApi.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CompanyProfile.Core.Exceptions;
using OwaspHeaders.Core.Extensions;
using System;
using System.IO;
using System.Net;

namespace CompanyProfile.Web.Api.Infrastructure
{
    public static class ConfigureAppExtentions
    {
        /// <summary>
        /// Configure Secure Header middleware per OWASP recommendations
        /// </summary>
        public static void ConfigureSecureHeaders(this IApplicationBuilder app, IConfiguration configuration)
        {
            var secureHeaderConfiguration = SecureHeaderExtensions.ConfigureFromSettings(configuration.GetSection("SecureHeadersConfiguration").Get<SecureHeadersConfiguration>());
            app.UseSecureHeadersMiddleware(secureHeaderConfiguration);
        }

        /// <summary>
        /// Configure the API exception handler
        /// </summary>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IConfiguration configuration)
        {
            var policy = configuration.GetSection("ExceptionPolicy")?.Get<ExceptionPolicy>();

            if (policy == null)
                policy = ExceptionPolicy.CreateDefault();

            policy.Register<ApplicationException>(HttpStatusCode.BadRequest)
                .Register<FileNotFoundException>(HttpStatusCode.NotFound)
                .Register<ASI.Sugar.Exceptions.ConfigurationException>(HttpStatusCode.InternalServerError)
                .Register<ASI.Sugar.Exceptions.BadRequestException>(HttpStatusCode.BadRequest)
                .Register<ASI.Sugar.Exceptions.DuplicateEntityException>(HttpStatusCode.Conflict)
                .Register<ASI.Sugar.Exceptions.EntityNotFoundException>(HttpStatusCode.NotFound)
                .Register<ASI.Sugar.Exceptions.EntityValidationException>(HttpStatusCode.BadRequest)
                .Register<MyEntityUpdateException>(HttpStatusCode.InternalServerError)
                ;

            app.UseExceptionHandler(new ExceptionLogger(), policy);
        }

        /// <summary>
        /// Configure Logging
        /// </summary>
        public static void ConfigureLogging(this IApplicationBuilder app, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            configuration.ConfigureEsbLogAppender();

            var logger = loggerFactory.CreateLogger("Startup");
            logger.LogInformation($"Starting API @ {DateTime.UtcNow}");
        }
    }
}
