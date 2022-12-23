using ProductProject.Entities;
using ProductProject.Interfaces;
using ProductProject.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductProject.Repositories;
using ProductProject.Model;
using System.Linq.Expressions;
using AutoMapper;

namespace ProductProject.Services
{

    public interface IProductService : IService<Product>
    {
        IQueryable<Product> GetProducts(GetProductRequestModel requestModel);
        void PostProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }


    public class ProductService : Service<Product>, IProductService
    {
        public IProductCategoryService ProductCategoryService { get; set; }

        public ProductService(IRepository<Product> repository, IUnitOfWork unitOfWork, IProductCategoryService productCategoryService) : base(repository, unitOfWork)
        {
            ProductCategoryService = productCategoryService;
        }

        public IQueryable<Product> GetProducts(GetProductRequestModel requestModel)
        {

            IQueryable<ProductCategory> productCategory = null;
            List<ProductAttribute> productAttributes = new List<ProductAttribute>();

            if (!string.IsNullOrEmpty(requestModel.CategoryName))
            {
                GetProductCategoryRequestModel productCategoryRequestModel = new GetProductCategoryRequestModel()
                {
                    Name = requestModel.CategoryName
                };

                productCategory = ProductCategoryService.GetProductCategories(productCategoryRequestModel);
            }

            var filters = new List<Expression<Func<Product, bool>>>();

            if (productCategory != null && productCategory.FirstOrDefault().Id > 0)
                filters.Add((Product x) => x.ProductCategoryId == productCategory.FirstOrDefault().Id);

            if (!string.IsNullOrEmpty(requestModel.Name))
                filters.Add((Product x) => x.Name.Contains(requestModel.Name));

            if (requestModel.ProductAttributes != null && requestModel.ProductAttributes.Count > 0)
            {
                foreach (var item in requestModel.ProductAttributes)
                {
                    ProductAttribute productAttribute = new ProductAttribute()
                    {
                        Name = item.Name,
                        Value = item.Value
                    };
                    productAttributes.Add(productAttribute);

                }
                filters.Add((Product x) => x.ProductAttributes == productAttributes);
            }
                

            if (requestModel.MinPrice > 0)
                filters.Add((Product x) => x.Price >= requestModel.MinPrice);

            if (requestModel.MaxPrice > 0)
                filters.Add((Product x) => x.Price <= requestModel.MaxPrice);

            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add((Product x) => x.ProductAttributes);

            IQueryable<Product> productList = _repository.Search(filters, includes);
            return productList;


        }

        public void PostProduct(Product entity)
        {
            Insert(entity);

        }

        public override void PreUpdate(Product entity)
        {
            base.PreUpdate(entity);
        }

        public void UpdateProduct(Product entity)
        {
            var includes = new List<Expression<Func<Product, object>>>();

            if (entity.ProductAttributes != null && entity.ProductAttributes.Count > 0)
                includes.Add((Product x) => x.ProductAttributes);

            var existingProduct = _repository.Load(entity.Id, includes);

            if (existingProduct == null)
                throw new Exception("Product not found");

            existingProduct.ProductCategoryId = entity.ProductCategoryId;
            existingProduct.Name = entity.Name;
            existingProduct.Price = entity.Price;
            existingProduct.ModifiedDateTime = DateTime.Now;
            existingProduct.ProductAttributes = entity.ProductAttributes;

            Update(existingProduct);

        }

        public void DeleteProduct(int id)
        {
            var existingProduct = _repository.Load(id);

            if (existingProduct == null)
                throw new Exception("Product not found");

            existingProduct.Deleted = 1;  // soft delete için veritabanında base olarak deleted kolonu tutulur.Bu kolondaki değer 1 yapılır ve soft delete sağlanmış olur.
            existingProduct.ModifiedDateTime = DateTime.Now;

            Update(existingProduct);
        }



    }
}
