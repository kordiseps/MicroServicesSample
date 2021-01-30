using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.MsSqlRepository.Base
{
    public abstract class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        public DbContext dbContext;
        private DbSet<TModel> dbSet;

        public GenericRepository(DbContext Context)
        {
            dbContext = Context;
            dbSet = dbContext.Set<TModel>();
        }


        public virtual long Count(Expression<Func<TModel, bool>> filter)
        {
            return dbSet.Where(filter).Count();
        }

        public virtual void Delete(object id)
        {
            Delete(GetById(id));
        }

        public virtual TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter)
        {
            return dbSet.Where(filter).FirstOrDefault();
        }

        public virtual List<TModel> GetList()
        {
            return dbSet.ToList();
        }

        public virtual TModel GetSingleOrDefault(Expression<Func<TModel, bool>> filter)
        {
            return dbSet.Where(filter).SingleOrDefault();
        }

        public virtual TModel GetById(object EntityId)
        {
            return dbSet.Find(EntityId);
        }
        public virtual IEnumerable<TModel> Filter(Expression<Func<TModel, bool>> Filter = null)
        {
            if (Filter != null)
            {
                return dbSet.Where(Filter);
            }
            return dbSet;
        }
        public virtual void Create(TModel entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }
        public virtual void Update(TModel entity)
        {

            foreach (var item in entity.GetType().GetProperties())
            {
                if (item.Name == "VERSION")
                {
                    item.SetValue(entity, (int)item.GetValue(entity) + 1);
                }
                if (item.Name == "LAST_UPDATE")
                {
                    item.SetValue(entity, DateTime.Now);
                }
            }
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public virtual void Delete(TModel Entity)
        {
            if (dbContext.Entry(Entity).State == EntityState.Detached) //Concurrency için 
            {
                dbSet.Attach(Entity);
            }
            dbSet.Remove(Entity);
            dbContext.SaveChanges();
        }

    }
}
