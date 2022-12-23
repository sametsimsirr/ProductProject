using ProductProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Model
{
    public class BaseModel
    {
        public decimal Id { get; set; }

        public string Description1 { get; set; }

        public ERecordState? RecordState { get; set; }
    }
}
