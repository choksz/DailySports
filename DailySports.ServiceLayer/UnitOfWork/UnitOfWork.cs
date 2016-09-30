using DailySports.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    context = new DailySportsContext();
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
