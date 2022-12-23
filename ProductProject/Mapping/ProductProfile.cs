using AutoMapper;
using ProductProject.Entities;
using ProductProject.Model;
using ProductProject.Model.Product;
using ProductProject.Model.ProductCategory;
using ProductProject.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<PostProductRequestModel, Product>();
            CreateMap<ProductAttribute, ProductAttributeModel>();
            CreateMap<ProductAttributeModel, ProductAttribute>();
            CreateMap<ProductCategory, ProductCategoryModel>();
            CreateMap<ProductCategoryModel, ProductCategory>();
            CreateMap<CategoryAttribute, CategoryAttributeModel>();
            CreateMap<CategoryAttributeModel, CategoryAttribute>();
            CreateMap<AttributeModel, Entities.Attribute>();
            CreateMap<Entities.Attribute, AttributeModel>();
        }
    }
}
