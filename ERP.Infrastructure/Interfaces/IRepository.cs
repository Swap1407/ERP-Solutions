using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        void Update(TEntity entity);
        TEntity GetByID(Guid id);
        Task<TEntity> GetByIDAsync(Guid id);
        void Insert(TEntity entity);
        Task<EntityEntry<TEntity>> InsertAsync(TEntity entity);
        void Delete(Guid id);
        void Delete(TEntity entityToDelete);
    }
}
