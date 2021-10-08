using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CompanyProfile.Core.Data;
using CompanyProfile.Data.Context;

namespace CompanyProfile.Data.Startup
{
    public static class DependencyInjection
    {
        public static void AddData(this IServiceCollection services)
        {
            services.AddDbContext<CompanyProfileDbContext>((sp, options) => options.UseSqlServer(sp
                .GetRequiredService<Core.Configuration.IConfiguration>()!.ConnectionString));

            services.AddScoped<IDataAccess, DataAccess>();
        }
    }
}
