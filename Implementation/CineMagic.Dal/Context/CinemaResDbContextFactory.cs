using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CinemaRes.Dal.Context
{
    public class CinemaResDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CinemaResDbContext>
    {
        public CinemaResDbContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder cfgBuilder = new ConfigurationBuilder();

            cfgBuilder.AddJsonFile("appSettings.Development.json", true);

            IConfigurationRoot configuration = cfgBuilder.Build();

            string connectionString = configuration.GetConnectionString(typeof(CinemaResDbContext).Name);

            DbContextOptionsBuilder<CinemaResDbContext> optionsBuilder = new DbContextOptionsBuilder<CinemaResDbContext>();

            optionsBuilder.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            optionsBuilder.EnableSensitiveDataLogging();

            return new CinemaResDbContext(optionsBuilder.Options);
        }
    }
}
