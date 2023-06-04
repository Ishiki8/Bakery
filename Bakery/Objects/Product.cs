using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Objects
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public List<Ordered_Product> OrderedProductEntities { get; set; }
    }
}
