using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class NameRatio
    {
        public string Name { get; set; }
        public float Ratio { get; set; }
        public NameRatio(string Name, float Ratio)
        {
            this.Name = Name;
            this.Ratio = Ratio;
        }
    }
}
