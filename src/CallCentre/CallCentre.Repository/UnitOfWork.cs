using CallCentre.Interfaces.Repository;
using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CallCentre.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext dataContext;

        private readonly Dictionary<Type, object> repositories;

        public UnitOfWork(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            repositories = new Dictionary<Type, object>();
        }

        public void SaveChanges()
        {
            dataContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        public IRepository<TSet> GetRepository<TSet>() where TSet : class, IBaseModel
        {
            if (repositories.ContainsKey(typeof(TSet))) return repositories[typeof(TSet)] as IRepository<TSet>;

            var repository = new UOWRepository<TSet>(dataContext);

            repositories.Add(typeof(TSet), repository);

            return repository;
        }
    }
}
