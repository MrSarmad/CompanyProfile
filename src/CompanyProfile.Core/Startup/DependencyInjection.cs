using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.Configuration;
using CompanyProfile.Core.Data;
using CompanyProfile.Core.MappingProfiles;
using CompanyProfile.Core.MyEntities;
using CompanyProfile.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CompanyProfile.Personify;

namespace CompanyProfile.Core.Startup
{
    public static class DependencyInjection
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MyEntityProfile).Assembly);

            services.AddSingleton<IConfiguration, FileConfiguration>();

            services.AddScoped<ISecurityPolicy, TenantSecurityPolicy>();
            services.AddScoped<IMyEntityLoader, MyEntityLoader>();
            services.AddScoped<IMyEntityService, MyEntityService>();

            services.AddScoped<IAuditFieldFixer, AuditFieldFixer>();

            services.AddScoped<IApiAccess, ApiAccess>();
        }

        /// <summary>
        /// Finds and registers all types that inherit from the provided type T
        /// </summary>
        /// <typeparam name="T">Type to find concrete implementations of</typeparam>
        /// <param name="services">ServiceCollection to register to</param>
        /// <param name="assemblies">Assemblies to search for implementations</param>
        /// <param name="lifetime">Lifetime of the registered services</param>
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes
                .Where(x => x.GetInterfaces().Contains(typeof(T)) && !x.IsInterface
            ));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }

        /// <summary>
        /// Removes all instances of type T from the provided service collection.
        /// This is especially useful for unit and integration tests
        /// </summary>
        /// <typeparam name="T">Type to remove</typeparam>
        /// <param name="services">Collection to remove registered type from</param>
        public static void EjectAllInstancesOf<T>(this IServiceCollection services)
        {
            ServiceDescriptor serviceDescriptor;
            do
            {
                serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
                if (serviceDescriptor != null)
                    services.Remove(serviceDescriptor);
            }
            while (serviceDescriptor != null);
        }

        /// <summary>
        /// Validates that every service added to the collection can be resolved.
        /// This will fail if you have scenarios where you have a Singleton that is
        /// expecting a service that is registered as Scoped, allowing you to find
        /// and fix the issue before runtime when the service tries to be resolved.
        ///
        /// Will throw AggregateException with any services that couldn't be resolved.
        /// </summary>
        /// <param name="services">Service collection to test</param>
        public static void AssertConfigurationIsValid(this IServiceCollection services)
        {
            var failedTypes = new List<string>();
            var exceptions = new List<Exception>();
            var prov = services.BuildServiceProvider();

            var skippedTypes = new[]
            {
                "Microsoft.Extensions.Options",
                "Microsoft.Extensions.Logging",
            };

            foreach (var service in services)
            {
                var typeName = service.ServiceType.FullName;
                try
                {
                    if (!skippedTypes.Any(y => typeName.Contains(y)))
                        prov.GetService(service.ServiceType);
                }
                catch (InvalidOperationException e)
                {
                    failedTypes.Add(typeName);
                    exceptions.Add(e);
                }
                catch (ArgumentException e)
                {
                    failedTypes.Add(typeName);
                    exceptions.Add(e);
                }
            }
            if (exceptions.Any())
            {
                throw new AggregateException("Some services are missing\n -" + string.Join("\n -", failedTypes), exceptions);
            }
        }
    }
}
