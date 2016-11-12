using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DailySports.DataLayer.Context
{
    public class DailySportsContextFactory : IDbContextFactory<DailySportsContext>
    {
        public DailySportsContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DailySportsContext>();
            builder.UseNpgsql("Host=104.197.193.197,Username=dailysports,Password=dailysports,Database=dailysports");
            return new DailySportsContext(builder.Options);
        }
    }
}