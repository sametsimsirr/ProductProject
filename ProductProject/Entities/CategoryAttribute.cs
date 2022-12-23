using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Entities
{
    public class CategoryAttribute:Base
    {
        [Column("Name")]
        public string Name { get; set; }

        [ForeignKey("CategoryAttributeId")]
        public virtual List<Attribute> Attributes { get; set; }
    }
}
