using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductProject.Interfaces;

namespace ProductProject.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBase
    {
        public readonly BaseDbContext _context;

        public BaseRepository(BaseDbContext context)
        {
            _context = context;
        }
        public void Add(TEntity instance)
        {
            instance.Deleted = 0;
            instance.CreatedDateTime = DateTime.Now;
            _context.Set<TEntity>().Add(instance);
            
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Update(TEntity instance)
        {
            _context.ChangeTracker.LazyLoadingEnabled = true;
            var existingParent = _context.Set<TEntity>().SingleOrDefault(p => p.Id == instance.Id && p.Deleted == 0);

            if (existingParent == null)
                throw new Exception("Main object not found for id: " + instance.Id);

            else
            {
                _context.Entry(instance);
            }

        }

        public IQueryable<TEntity> Search(
       List<Expression<Func<TEntity, bool>>> filters = null,
       List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = Queryable();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (filters != null)
                foreach (var filter in filters)
                    query = query.Where(filter);

            return query;

        }

        public IQueryable<TEntity> Queryable()
        {
            return Queryable(null);
        }

        public IQueryable<TEntity> Queryable(string[] Includes)
        {
            _context.ChangeTracker.LazyLoadingEnabled = true;
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (Includes != null)
                foreach (string include in Includes)
                    queryable = queryable.Include(include);

            return queryable;
        }

        public TEntity Load(int id)
        {
            return Queryable().FirstOrDefault(p => p.Id == id && p.Deleted == 0);
        }

        public TEntity Load(int id,List<Expression<Func<TEntity, object>>> includes = null)
        {
            var query = Queryable();

            foreach (var include in includes) query = query.Include(include);

            return query.Single(p => p.Id == id && p.Deleted == 0);
        }


    }
}
