using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductProject.Model;
using ProductProject.Entities;
using ProductProject.Interfaces;

namespace ProductProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    abstract public class BaseController<TEntity, TEntityService> : ControllerBase
         where TEntity : Base
        where TEntityService : IService<TEntity>
       
    {
        public TEntityService service;

        public BaseController(TEntityService Service)
        {
            Service = service;
        }
    }
    public interface IBaseModel
    {
        int Id { get; set; }
    }
}
