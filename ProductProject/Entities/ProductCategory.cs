using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Entities
{
    public class ProductCategory:Base
    {
        [Column("Name")]
        public string Name { get; set; }

        [ForeignKey("ProductCategoryId")]
        public virtual List<CategoryAttribute> CategoryAttributes { get; set; }
    }
}
