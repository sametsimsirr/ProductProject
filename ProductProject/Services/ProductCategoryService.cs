using AutoMapper;
using ProductProject.Entities;
using ProductProject.Interfaces;
using ProductProject.Model.ProductCategory;
using ProductProject.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductProject.Services
{
    public interface IProductCategoryService : IService<ProductCategory>
    {
        IQueryable<ProductCategory> GetProductCategories(GetProductCategoryRequestModel requestModel);
        void PostProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(ProductCategory productCategory);
        void DeleteProductCategory(int id);
    }

    public class ProductCategoryService : Service<ProductCategory>, IProductCategoryService
    {
        public IMapper _mapper;

        public ProductCategoryService(IRepository<ProductCategory> repository, IUnitOfWork unitOfWork,IMapper mapper)  : base(repository, unitOfWork)
        {
            _mapper = mapper;
        }

        public IQueryable<ProductCategory> GetProductCategories(GetProductCategoryRequestModel requestModel)
        {
            List<CategoryAttribute> categoryAttributes = new List<CategoryAttribute>();
            var filters = new List<Expression<Func<ProductCategory, bool>>>();

            if (!string.IsNullOrEmpty(requestModel.Name))
                filters.Add((ProductCategory x) => x.Name.Contains(requestModel.Name));

            if (requestModel.CategoryAttributes != null && requestModel.CategoryAttributes.Count > 0)
            {
                categoryAttributes = _mapper.Map<List<CategoryAttribute>>(requestModel.CategoryAttributes);
                filters.Add((ProductCategory x) => x.CategoryAttributes == categoryAttributes);
            }

            var includes = new List<Expression<Func<ProductCategory, object>>>();
            includes.Add((ProductCategory x) => x.CategoryAttributes);
            IQueryable<ProductCategory> productCategoryList = _repository.Search(filters, includes);

            return productCategoryList;
        }

        public void PostProductCategory(ProductCategory entity)
        {
            Insert(entity);

        }

        public override void PreUpdate(ProductCategory entity)
        {
            base.PreUpdate(entity);
        }

        public void UpdateProductCategory(ProductCategory entity)
        {
            var includes = new List<Expression<Func<ProductCategory, object>>>();
            var existingProductCategory = new ProductCategory();

            if (entity.CategoryAttributes != null && entity.CategoryAttributes.Count > 0)
            {
                includes.Add((ProductCategory x) => x.CategoryAttributes);
                existingProductCategory = _repository.Load(entity.Id, includes);
            }
            else
                existingProductCategory = _repository.Load(entity.Id);


            if (existingProductCategory == null)
                throw new Exception("Product not found");

            existingProductCategory.Name = entity.Name;
            existingProductCategory.CategoryAttributes = entity.CategoryAttributes;
            existingProductCategory.ModifiedDateTime = DateTime.Now;

            Update(existingProductCategory);

        }

        public void DeleteProductCategory(int id)
        {
            var existingProductCategory = _repository.Load(id);

            if (existingProductCategory == null)
                throw new Exception("Product not found");

            existingProductCategory.Deleted = 1;  // soft delete için veritabanında base olarak deleted kolonu tutulur.Bu kolondaki değer 1 yapılır ve soft delete sağlanmış olur.
            existingProductCategory.ModifiedDateTime = DateTime.Now;

            Update(existingProductCategory);
        }

    }
}
