using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Objects
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<User> UserEntities { get; set; }
    }
}
