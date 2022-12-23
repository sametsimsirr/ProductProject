using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Model.ProductCategory
{
    public class CategoryAttributeModel: BaseModel
    {
        public string Name { get; set; }
        public  List<AttributeModel> Attributes { get; set; }
    }
}
