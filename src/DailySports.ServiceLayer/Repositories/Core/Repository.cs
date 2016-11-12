using DailySports.DataLayer.Context;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.ServiceLayer.Utilities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace DailySports.ServiceLayer.Repositories.Core
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DailySportsContext DbContext;
        protected DbSet<TEntity> entities;
        private DbSet<TEntity> Entities
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
            IQueryable<TEntity> query = Entities;
            return query;
        }
        public TEntity Find(int id)
        {
            return Entities.Find(id);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = Entities.Where(predicate);
            return query;
        }

        public EntityEntry<TEntity> Add(TEntity entity)
        {
            var result = Entities.Add(entity);
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
