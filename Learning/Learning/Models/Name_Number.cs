using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class Name_Number
    {
        public string Name { get; set; }
        public float Number { get; set; }
        public Name_Number(string Name, float Number)
        {
            this.Name = Name;
            this.Number = Number;
        }
    }
}
