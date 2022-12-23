using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Entities
{
    public class Attribute:Base
    {
        [Column("Value")]
        public string Value { get; set; }

    }
}
