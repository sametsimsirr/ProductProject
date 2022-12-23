using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductProject.Model
{
    public class RangeSearchModel<T>
    {
        public T BeginValue { get; set; }
        public T EndValue { get; set; }
        public T Value { get; set; }
    }

}
