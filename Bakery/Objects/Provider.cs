﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakery.Objects
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ITN { get; set; }
        public string PhoneNumber { get; set; }
        public List<Supply> SupplyEntities { get; set; }
    }
}
