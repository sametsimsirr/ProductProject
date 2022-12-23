using ProductProject.Entities;
using ProductProject.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Model.RequestModel
{
    public class GetProductRequestModel : BaseModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public List<ProductAttributeModel> ProductAttributes { get; set; }
    }
}
