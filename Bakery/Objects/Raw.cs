using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Objects
{
    public class Raw
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string InStock { get; set; }
        public List<Supplied_Raw> SuppliedRawEntities { get; set; }
    }
}
