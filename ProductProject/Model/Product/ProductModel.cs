using ProductProject.Model.Product;
using ProductProject.Model.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Model
{
    public class ProductModel:BaseModel
    {
        public int? ProductCategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        
        public List<ProductAttributeModel> ProductAttributes { get; set; }
        public ProductCategoryModel ProductCategory { get; set; }
    }
}
