using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductProject.Entities;
using ProductProject.Interfaces;
using ProductProject.Model.ProductCategory;
using ProductProject.Model.RequestModel;
using ProductProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseController<Entities.ProductCategory, IProductCategoryService>
    {
        private readonly IMapper _mapper;
        private readonly IProductCategoryService _service;

        public ProductCategoryController(IProductCategoryService Service, IMapper mapper) : base(Service)
        {
            _mapper = mapper;
            _service = Service;
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetProductCatalogs([FromQuery] GetProductCategoryRequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                    return BadRequest(requestModel);

                if (string.IsNullOrEmpty(requestModel.Name) && requestModel.CategoryAttributes == null)
                    return BadRequest(requestModel);

                List<ProductCategoryModel> productCategories = new List<ProductCategoryModel>();

                productCategories = _mapper.Map<List<ProductCategoryModel>>(_service.GetProductCategories(requestModel));

                if (productCategories == null || productCategories.Count == 0)
                    return NotFound(requestModel);
                else
                    return Ok(productCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult PostProductCatalog(PostProductCategoryRequestModel requestModel)
        {
            try
            {
                if (requestModel.ProductCategory == null)
                    return BadRequest();

                var productCategory = _mapper.Map<ProductCategory>(requestModel.ProductCategory);
                _service.PostProductCategory(productCategory);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult PutProductCatalog(PutProductCategoryRequestModel requestModel)
        {
            try
            {
                if (requestModel.ProductCategory == null || requestModel.ProductCategory.Id == 0)
                    return BadRequest();

                var productCategory = _mapper.Map<ProductCategory>(requestModel.ProductCategory);
                _service.UpdateProductCategory(productCategory);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteProductCatalog(DeleteProductCategoryRequestModel requestModel)
        {
            try
            {
                if (requestModel.Id == 0)
                    return BadRequest();

                _service.DeleteProductCategory(requestModel.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }


    }
}
