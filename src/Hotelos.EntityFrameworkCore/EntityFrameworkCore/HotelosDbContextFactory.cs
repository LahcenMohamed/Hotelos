using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Hotelos.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class HotelosDbContextFactory : IDesignTimeDbContextFactory<HotelosDbContext>
{
    public HotelosDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        HotelosEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<HotelosDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new HotelosDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Hotelos.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
