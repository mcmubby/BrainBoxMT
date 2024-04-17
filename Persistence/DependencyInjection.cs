using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddDatabaseServices(this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();

            var dbHost = "localhost"; //Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = "brainboxdb"; //Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = "postgres"; //Environment.GetEnvironmentVariable("DB_USER");
            var dbPass = "postgres";// Environment.GetEnvironmentVariable("DB_PASSWORD");
            
            var connectionString = $"Host={dbHost};Port=5432;Database={dbName};Username={dbUser};Password={dbPass};Include Error Detail=true;Trust Server Certificate=true";

            services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connectionString));

        }
    }
}
