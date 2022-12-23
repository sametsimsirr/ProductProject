using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Interfaces
{
    public interface IBase
    {
        int Id { get; set; }
        string Description { get; set; }
        int? Deleted { get; set; }
        DateTime? CreatedDateTime { get; set; }
        int? CreatedUser { get; set; }
        DateTime? ModifiedDateTime { get; set; }
        int? ModifiedUser { get; set; }
    }
}
