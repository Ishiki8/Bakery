using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Objects
{
    public class Supplied_Raw
    {
        public int SupplyId { get; set; }
        public int RawId { get; set; }
        public int Quantity { get; set; }
        public Supply SupplyEntity { get; set; }
        public Raw RawEntity { get; set; }
    }
}
