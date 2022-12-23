using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductProject.Entities;
using ProductProject.Interfaces;
using ProductProject.Model;
using ProductProject.Model.Product;
using ProductProject.Model.RequestModel;
using ProductProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController<Entities.Product, IProductService>
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;


        public ProductController(IProductService Service, IMapper mapper) : base(Service)
        {
            _mapper = mapper;
            _service = Service;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProduct([FromQuery]GetProductRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                    return BadRequest(requestModel);

                if (string.IsNullOrEmpty(requestModel.Name) && string.IsNullOrEmpty(requestModel.CategoryName) && requestModel.ProductAttributes == null
                    && requestModel.MinPrice <= 0 && requestModel.MaxPrice <= 0)
                    return BadRequest(requestModel);

                List<ProductModel> products = new List<ProductModel>();
                products = _mapper.Map<List<ProductModel>>(_service.GetProducts(requestModel));

                if (products == null || products.Count == 0)
                {
                    return NotFound(requestModel);
                }
                else
                {
                    return Ok(products);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult PostProduct(PostProductRequestModel requestModel)
        {
            try
            {
                if (requestModel.Product == null)
                    return BadRequest();

                var product = _mapper.Map<Product>(requestModel.Product);
                _service.PostProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult PutProduct(PutProductRequestModel requestModel)
        {
            try
            {
                if (requestModel.Product == null || requestModel.Product.Id == 0)
                    return BadRequest();

                var product = _mapper.Map<Product>(requestModel.Product);
                _service.UpdateProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteProduct(DeleteProductRequestModel requestModel)
        {
            try
            {
                if (requestModel.Id == 0)
                    return BadRequest();

                _service.DeleteProduct(requestModel.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

    }
}
