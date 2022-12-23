using AutoMapper;
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
    public class ProductCategoryServiceTest
    {

        [Fact]
        public void Constructor_Should_Create_Service()
        {
            var mockRepo = Substitute.For<IRepository<ProductCategory>>();
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockMapper = Substitute.For<IMapper>();
            var service = new ProductCategoryService(mockRepo, mockUnit,mockMapper);

            Assert.NotNull(service);
        }

        [Fact]
        public void GetProductCategories_ReturnsMatchingProductCategories_WhenRequestModelIsValid()
        {
            GetProductCategoryRequestModel getProductCategoryRequestModel = new GetProductCategoryRequestModel()
            {
                Name = "TShirtCategory"
            };

            var mockRepo = Substitute.For<IRepository<ProductCategory>>();
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockMapper = Substitute.For<IMapper>();
            var service = new ProductCategoryService(mockRepo, mockUnit, mockMapper);
            var products = service.GetProductCategories(getProductCategoryRequestModel);

            Assert.NotNull(products);


        }

        [Fact]
        public void GetProducts_ReturnsEmptyList_WhenRequestModelIsInvalid()
        {
            var mockRepo = Substitute.For<IRepository<ProductCategory>>();
            var mockUnit = Substitute.For<IUnitOfWork>();
            var mockMapper = Substitute.For<IMapper>();
            var service = new ProductCategoryService(mockRepo, mockUnit, mockMapper);

            var requestModel = new GetProductCategoryRequestModel
            {
                Name = null,
                CategoryAttributes = null
            };

            var products = service.GetProductCategories(requestModel);

            Assert.NotNull(products);
            Assert.Empty(products);
        }

    }
}
