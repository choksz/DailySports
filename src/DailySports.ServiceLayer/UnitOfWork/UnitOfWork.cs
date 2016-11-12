using DailySports.DataLayer.Context;
using System;
using Microsoft.EntityFrameworkCore;

namespace DailySports.ServiceLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DailySportsContext context;

        public UnitOfWork()
        {
        }

        public DailySportsContext DbContext
        {
            get
            {
                if (context == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<DailySportsContext>();
                    context = new DailySportsContext(optionsBuilder.Options);
                }
                return context;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
