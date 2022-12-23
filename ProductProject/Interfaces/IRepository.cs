using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductProject.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBase
    {
        void Dispose(bool disposing);
        void Dispose();
        void Add(TEntity instance);
        IQueryable<TEntity> Search(
        List<Expression<Func<TEntity, bool>>> filters = null,
        List<Expression<Func<TEntity, object>>> includes = null);
        TEntity Load(int id);
        TEntity Load(int id,List<Expression<Func<TEntity, object>>> includes = null);
        void Update(TEntity instance);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> Queryable(string[] includes);
        void SaveChanges();
    }
    public interface IRepository
    {

    }
}
