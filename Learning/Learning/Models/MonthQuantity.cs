using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Models
{
    public class MonthQuantity
    {
        public int Month { get; set; }
        public int Quantity { get; set; }
        public MonthQuantity(int Month, int Quantity)
        {
            this.Month = Month;
            this.Quantity = Quantity;
        }
    }
}
