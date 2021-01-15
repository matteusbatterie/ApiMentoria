using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System;
using System.IO;

namespace Repository.Configuration
{
    public class MentoriaContextFactory : IDesignTimeDbContextFactory<MentoriaContext>
    {
        public MentoriaContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<MentoriaContext>();

            var connectionString = configuration.GetConnectionString("dbConnection");
            builder.UseSqlServer(connectionString);

            return new MentoriaContext(builder.Options);
        }
    }
}
