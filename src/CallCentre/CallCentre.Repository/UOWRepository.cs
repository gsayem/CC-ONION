using CallCentre.Interfaces.Repository;
using CallCentre.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CallCentre.Repository
{
    public class UOWRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseModel
    {
        public UOWRepository(IDataContext dataContext)
        {
            if (dataContext == null) throw new ArgumentNullException(nameof(dataContext));

            this.DataContext = dataContext;
        }

        public IDataContext DataContext { get; }

        public virtual void Add(TEntity entity)
        {
            DataContext.Set<TEntity>().Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DataContext.Entry(entity).State = EntityState.Deleted;
        }

        public TEntity GetById(Guid id)
        {
            return DataContext.Set<TEntity>().SingleOrDefault(i => i.Id == id);
        }

        public virtual IList<TEntity> GetAll()
        {
            return DataContext.Set<TEntity>().ToList();
        }

        public virtual void Update(TEntity entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual int Count()
        {
            return DataContext.Set<TEntity>().Count();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().Where(predicate).ToList();
        }

        public void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}
