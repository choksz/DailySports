using DailySports.DataLayer.Context;
using DailySports.ServiceLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Repositories.Core
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DailySportsContext DbContext;
        protected IDbSet<TEntity> entities;
        private IDbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = DbContext.Set<TEntity>();
                }
                return entities;
            }
        }

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.DbContext = unitOfWork.DbContext;
        }
        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = this.DbContext.Set<TEntity>();
            return query;
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = this.DbContext.Set<TEntity>().Where(predicate);
            return query;
        }
        public TEntity Add(TEntity entity)
        {
           TEntity result = this.Entities.Add(entity);
            this.DbContext.SaveChanges();
            return result;
        }
        public void Delete(TEntity entity)
        {
            Entities.Remove(entity);
            this.DbContext.SaveChanges();
            
        }
        

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
    }
}
