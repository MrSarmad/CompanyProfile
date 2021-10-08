using ASI.Services.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CompanyProfile.Web.Api.Infrastructure
{
    /// <summary>
    /// Swagger setup methods
    /// </summary>
    public static class SwaggerExtensions
    {
        public const string AppName = "CompanyProfile";

        /// <summary>
        /// Configure the Swagger endpoints for this app
        /// </summary>
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            //Enable Swagger API documentation support and SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} v1");
            });
        }

        /// <summary>
        /// Register's swagger using ASI's standards
        /// </summary>
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //Update Swagger Documentation Title as per your API naming convention
                options.SwaggerDoc("v1", new OpenApiInfo { Title = $"ASI {AppName}", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    Description = "Enter or paste your JWT here",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                var requirement = new OpenApiSecurityRequirement();
                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                requirement[scheme] = new List<string>();
                options.AddSecurityRequirement(requirement);

                // If you annotate Controllers and API Types with
                // Xml comments (http://msdn.microsoft.com/en-us/library/b2s063f7(v=vs.110).aspx), you can incorporate
                // those comments into the generated docs and UI. You can enable this by providing the path to one or
                // more Xml comment files. 
                // Below code assumes the XML documentation file name is AssemblyName.xml and in the same folder. Update as per need.
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                if (File.Exists(xmlCommentsFullPath))
                {
                    options.IncludeXmlComments(xmlCommentsFullPath);
                }
            });
        }
    }
}
