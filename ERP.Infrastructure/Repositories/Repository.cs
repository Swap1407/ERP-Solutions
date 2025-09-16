using ERP.Infrastructure.Data;
using ERP.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace ERP.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ERPDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(ERPDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual TEntity GetByID(Guid id)
        {
            return dbSet.Find(id);
        }
        public virtual async Task<TEntity> GetByIDAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual void Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            dbSet.Add(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return await dbSet.AddAsync(entity);
        }

        public virtual void Delete(Guid id)
        {
            var entityToDelete = dbSet.Find(id);
            if (entityToDelete == null) throw new KeyNotFoundException($"Entity with id {id} not found.");
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null) throw new ArgumentNullException(nameof(entityToUpdate));
            dbSet.Update(entityToUpdate);
        }
    }

}
