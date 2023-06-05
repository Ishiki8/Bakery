using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Objects
{
    public class Supply
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int ProviderId { get; set; }
        public Provider ProviderEntity { get; set; }
        public List<Supplied_Raw> SuppliedRawEntities { get; set; }
    }
}
