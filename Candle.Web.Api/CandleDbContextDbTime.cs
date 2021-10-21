using Candle.InfraStructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Candle.Web.Api
{
    public class CandleDbContextDbTime : IDesignTimeDbContextFactory<CandleDbContext>
    {
        public CandleDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var builder = new DbContextOptionsBuilder<CandleDbContext>();
            var connectionString = configuration.GetConnectionString("LocalConnectionString");

            builder.UseNpgsql(connectionString);

            return new CandleDbContext();
        }

    }
}
