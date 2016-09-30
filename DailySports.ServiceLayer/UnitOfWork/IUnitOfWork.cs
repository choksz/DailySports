using DailySports.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DailySportsContext DbContext { get; }
        int Save();
    }
}
