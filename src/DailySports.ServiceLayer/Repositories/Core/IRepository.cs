using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DailySports.ServiceLayer.Repositories.Core
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity Find(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        EntityEntry<TEntity> Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
