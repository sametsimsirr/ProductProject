using ProductProject.Entities;
using ProductProject.Model.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Model.RequestModel
{
    public class GetProductCategoryRequestModel
    {
        public string Name { get; set; }
        public List<CategoryAttributeModel> CategoryAttributes { get; set; }
    }
}
