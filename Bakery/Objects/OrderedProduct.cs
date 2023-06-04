using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Objects
{
    public class Ordered_Product
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Order OrderEntity { get; set; }
        public Product ProductEntity { get; set; }
    }
}
