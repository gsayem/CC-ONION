using CallCentre.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CallCentre.Interfaces.Repository
{
    public interface IRepositoryAsync<TEntity> where TEntity : class, IBaseModel
    {
        Task<Guid> AddAsync(TEntity entity);

        Task<int> RemoveAsync(TEntity entity);

        Task<IList<TEntity>> GetAllAsync();

        Task<int> UpdateAsync(TEntity entity);

        Task<int> CountAsync();

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
