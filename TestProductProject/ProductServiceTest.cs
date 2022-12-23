using Moq;
using NSubstitute;
using ProductProject.Entities;
using ProductProject.Interfaces;
using ProductProject.Model.RequestModel;
using ProductProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace TestProductProject
{
    public class ProductServiceTest
    {

        [Fact]
        public void Constructor_Should_Create_Service()
        {
            var mockRepo = Substitute.For<IRepository<Product>>();
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockCategoryService = Substitute.For<IProductCategoryService>();
            var service = new ProductService(mockRepo, mockUnit, mockCategoryService);

            Assert.NotNull(service);
        }


        [Fact]
        public void GetProducts_ReturnsMatchingProducts_WhenRequestModelIsValid()
        {
            GetProductRequestModel getProductRequestModel = new GetProductRequestModel()
            {
                Name = "Test1"
            };

            var mockRepo = Substitute.For<IRepository<Product>>();
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockCategoryService = Substitute.For<IProductCategoryService>();
            var service = new ProductService(mockRepo, mockUnit, mockCategoryService);
            var products = service.GetProducts(getProductRequestModel);

            Assert.NotNull(products);


        }

        [Fact]
        public void GetProducts_ReturnsEmptyList_WhenRequestModelIsInvalid()
        {
            var mockRepo = Substitute.For<IRepository<Product>>();
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockCategoryService = Substitute.For<IProductCategoryService>();
            var service = new ProductService(mockRepo, mockUnit, mockCategoryService);

            var requestModel = new GetProductRequestModel
            {
                Name = null,
                CategoryName = null,
                ProductAttributes = null,
                MinPrice = -1,
                MaxPrice = -1
            };

            var products = service.GetProducts(requestModel);

            Assert.NotNull(products);
            Assert.Empty(products);
        }

        [Fact]
        public void PostProduct_AddsProductToRepository_WhenProductIsValid()
        {
            var mockRepo = new Mock<IRepository<Product>>();
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockCategoryService = Substitute.For<IProductCategoryService>();
            var service = new ProductService(mockRepo.Object, mockUnit, mockCategoryService);

            var product = new Product
            {
                Name = "Product1",
                Price = 10,
                ProductCategoryId = 1,
                ProductAttributes = new List<ProductAttribute>
                {
                    new ProductAttribute { Name = "Size", Value = "M" },
                    new ProductAttribute { Name = "Color", Value = "White" },
                }
            };

            service.PostProduct(product);

            mockRepo.Verify(r => r.Add(It.Is<Product>(p => p.Name == "Product1"
                && p.Price == 10
                && p.ProductCategoryId == 1
                && p.ProductAttributes.Any(pa => pa.Name == "Size" && pa.Value == "M")
                && p.ProductAttributes.Any(pa => pa.Name == "Color" && pa.Value == "White"))), Times.Once());


        }


        [Fact]
        public void DeleteProduct_UpdatesProductInRepository_WhenProductIsFound()
        {
            var mockRepo = new Mock<IRepository<Product>>();
            mockRepo.Setup(r => r.Load(1)).Returns(new Product
            {
                Id = 1,
                Name = "Test",
                Price = 500,
                ProductCategoryId = 1,
                ProductAttributes = new List<ProductAttribute>
                {
                    new ProductAttribute { Name = "Size", Value = "M" },
                }
            });
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockCategoryService = Substitute.For<IProductCategoryService>();
            var service = new ProductService(mockRepo.Object, mockUnit, mockCategoryService);

            service.DeleteProduct(1);

            mockRepo.Verify(r => r.Update(It.Is<Product>(p => p.Id == 1 && p.Deleted == 1)));
        }

        [Fact]
        public void UpdateProduct_ThrowsException_WhenProductIsNotFound()
        {
            var mockRepo = new Mock<IRepository<Product>>();
            mockRepo.Setup(r => r.Load(1)).Returns((Product)null);
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockCategoryService = Substitute.For<IProductCategoryService>();
            var service = new ProductService(mockRepo.Object, mockUnit, mockCategoryService);

            var product = new Product
            {
                Id = 1,
                Name = "UpdatedProduct",
                Price = 20,
                ProductCategoryId = 2,
                ProductAttributes = new List<ProductAttribute>
                {
                    new ProductAttribute { Name = "Size", Value = "S" },
                    new ProductAttribute { Name = "Color", Value = "Green" }
                }
            };



            Assert.Throws<Exception>(() => service.UpdateProduct(product));
        }

    }
}
