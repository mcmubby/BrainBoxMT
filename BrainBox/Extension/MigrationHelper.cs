using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace BrainBox.Extension
{
    public static class MigrationHelper
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
