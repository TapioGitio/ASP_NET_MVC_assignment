using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Context;

public class DataContextFactory(IConfiguration configuration) : IDesignTimeDbContextFactory<DataContext>
{
    private readonly IConfiguration _configuration = configuration;

    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var connectionString = _configuration.GetConnectionString("AlphaConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new DataContext(optionsBuilder.Options);
    }
}
