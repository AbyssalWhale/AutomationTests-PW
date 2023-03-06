using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.ZephyrScale.Cycles
{
    public class Links
    {
        public string self { get; set; }
        public List<object> issues { get; set; }
        public List<object> webLinks { get; set; }
        public List<object> testPlans { get; set; }
    }
}
