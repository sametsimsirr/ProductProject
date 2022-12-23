using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductProject.Interfaces
{
    public interface IService<TEntity> where TEntity : IBase
    {
        TEntity Get(int id);
        void Commit();
        void Rollback();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Update(List<TEntity> entities);
        void Delete(TEntity entity);
        IQueryable<TEntity> Search(
        List<Expression<Func<TEntity, bool>>> filters = null,
        List<Expression<Func<TEntity, object>>> includes = null);
    }
}
