using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlphaWebApp.Identity.Context
{
    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("IdentityConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }
}