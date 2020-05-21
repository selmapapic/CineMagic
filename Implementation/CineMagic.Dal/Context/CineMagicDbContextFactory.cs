using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineMagic.Dal.Context
{
    public class CinemaResDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CineMagicDbContext>
    {
        public CineMagicDbContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder cfgBuilder = new ConfigurationBuilder();

            cfgBuilder.AddJsonFile("appSettings.Development.json", true);

            IConfigurationRoot configuration = cfgBuilder.Build();

            string connectionString = configuration.GetConnectionString(typeof(CineMagicDbContext).Name);

            DbContextOptionsBuilder<CineMagicDbContext> optionsBuilder = new DbContextOptionsBuilder<CineMagicDbContext>();

            optionsBuilder.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            optionsBuilder.EnableSensitiveDataLogging();

            return new CineMagicDbContext(optionsBuilder.Options);
        }
    }
}
