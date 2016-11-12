using DailySports.DataLayer.Context;
using System;

namespace DailySports.ServiceLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DailySportsContext DbContext { get; }
        int Save();
    }
}
