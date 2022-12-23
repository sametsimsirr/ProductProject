using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ProductProject.Controllers;
using ProductProject.Entities;
using ProductProject.Model;
using ProductProject.Model.RequestModel;
using ProductProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProductProject
{
    public class ProductControllerTest
    {

        [Fact]
        public void Constructor_Should_Create_Controller()
        {
            // Arrange
            var mockMapper = Substitute.For<IMapper>();
            var mockService = Substitute.For<IProductService>();

            // Act
            var controller = new ProductController(mockService, mockMapper);

            // Assert
            Assert.NotNull(controller);
        }

        

        [Fact]
        public void GetProduct_ReturnsBadRequest_WhenRequestModelIsNull()
        {
            
            var mockMapper = Substitute.For<IMapper>();
            var mockService = Substitute.For<IProductService>();
            var controller = new ProductController(mockService, mockMapper);


            var result = controller.GetProduct(null);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);

        }

        [Fact]
        public void GetProduct_ReturnsNotFound_WhenMatchingProductsAreFound()
        {

            var mockMapper = Substitute.For<IMapper>();
            var mockService = Substitute.For<IProductService>();
            var controller = new ProductController(mockService, mockMapper);
            var requestModel = new GetProductRequestModel() { Name = "Samet" };

            var result = controller.GetProduct(requestModel);
            var notFoundkRequestResult = result as NotFoundObjectResult;
            Assert.Equal(404, notFoundkRequestResult.StatusCode);

        }

    }
}
