using DailySports.DataLayer.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DailySports.DataLayer.Context
{
    
    public class DailySportsContextFactory : IDbContextFactory<DailySportsContext>
    {
        
        public DailySportsContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DailySportsContext>();
            builder.UseNpgsql(AppSettings.CONNECTION_STRING);
            return new DailySportsContext(builder.Options);
        }
        
    }
    
}
