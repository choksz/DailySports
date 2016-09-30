using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Context
{
   public class DailySportsInitializer : DropCreateDatabaseIfModelChanges<DailySportsContext>
    {
        protected override void Seed(DailySportsContext context)
        {
            context.SaveChanges();
        }
    }
}
