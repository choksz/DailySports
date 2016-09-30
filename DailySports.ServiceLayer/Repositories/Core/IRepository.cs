using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Repositories.Core
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
