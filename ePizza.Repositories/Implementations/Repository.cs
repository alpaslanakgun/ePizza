using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ePizza.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ePizza.Repositories.Implementations
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity : class
    {


        protected  DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }
        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

            }
            return await query.SingleOrDefaultAsync();
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            //Remove bir async olmadıgından dolayı burada Task.Run ile kendimiz olusturmus olduk...
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
            return entity;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
