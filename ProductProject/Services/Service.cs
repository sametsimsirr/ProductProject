using ProductProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductProject.Services
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IBase
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        protected Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual TEntity Get(int id)
        {
            return _repository.Load(id);
        }

        public virtual void Insert(TEntity entity)
        {
            PreInsert(entity);
            PreSave(entity);

            _repository.Add(entity);
            _repository.SaveChanges();
            _unitOfWork.Save();

            PostInsert(entity);
            PostSave(entity);

        }

        public virtual void Update(TEntity entity)
        {
            Update(new List<TEntity> { entity });
        }

        public virtual void Update(List<TEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                PreUpdate(entities[i]);
                PreSave(entities[i]);

                _repository.Update(entities[i]);

            }
            _repository.SaveChanges();
            _unitOfWork.Save();

            for (int i = 0; i < entities.Count; i++)
            {
                PostUpdate(entities[i]);
                PostSave(entities[i]);
            }

        }

        public virtual void Delete(TEntity entity)
        {
            entity.Deleted = 1;
            Update(entity);
        }

        public virtual IQueryable<TEntity> Search(
          List<Expression<Func<TEntity, bool>>> filters = null,
          List<Expression<Func<TEntity, object>>> includes = null)
        {
            return _repository.Search(filters, includes);
        }


        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Rollback()
        {
            _unitOfWork.Rollback();
        }

        /// <summary>
        /// Yeni kayıt öncesi Entityler kaydedilmeden çalışır. 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PreInsert(TEntity entity)
        {

        }

        /// <summary>
        /// Entityler kaydedilmeden önce çalışır. (Yeni kayıtta ve güncelleme öncesi)
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PreSave(TEntity entity)
        {

        }

        /// <summary>
        /// Yeni kayıt sonrasında çalışır.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PostInsert(TEntity entity)
        {

        }

        /// <summary>
        /// Yeni kayıt ve güncelleme sonrasında çalışır
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PostSave(TEntity entity)
        {

        }

        /// <summary>
        /// Güncelleme öncesi Entityler kaydedilmeden çalışır. 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PreUpdate(TEntity entity)
        {

        }

        /// <summary>
        /// Güncelleme sonrasında çalışır
        /// </summary>
        /// <param name="entity"></param>
        public virtual void PostUpdate(TEntity entity)
        {

        }

    }
}
