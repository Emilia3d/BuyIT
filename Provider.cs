﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyIT
{
    public class Provider
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}
