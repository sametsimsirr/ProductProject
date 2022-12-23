using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Entities
{
    public class Product:Base
    {
       
        [Column("Name")]
        public string Name { get; set; }

        [Column("Product_Category_Id")]
        public int? ProductCategoryId { get; set; }

        [Column("Price")]
        public double Price { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<ProductAttribute> ProductAttributes { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }


    }
}
