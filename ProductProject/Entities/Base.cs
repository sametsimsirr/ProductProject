using ProductProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Entities
{
    public class Base : IBase
    {
        #region props  

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }


        [StringLength(255), Column("Description")]
        public string Description { get; set; }

        [Column("Created_User")]
        public int? CreatedUser { get; set; }

        [Column("Created_Datetime")]
        public DateTime? CreatedDateTime { get; set; }

        [Column("Modified_User")]
        public int? ModifiedUser { get; set; }

        [Column("Modified_Datetime")]
        public DateTime? ModifiedDateTime { get; set; }

        [Column("Deleted")]
        public int? Deleted { get; set; }


        #endregion

        #region const

        public Base()
        {
            CreatedDateTime = DateTime.Now;
            Deleted = 0;
        }

        #endregion
    }
}
