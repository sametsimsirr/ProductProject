using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Model.ProductCategory
{
    public class ProductCategoryModel: BaseModel
    {
        public string Name { get; set; }
        public  List<CategoryAttributeModel> CategoryAttributes { get; set; }
    }
}
