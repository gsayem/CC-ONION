using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CallCentre.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
        IRepository<TSet> GetRepository<TSet>() where TSet : class, IBaseModel;        
    }
}
