﻿using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Shop.Models
{
    [Serializable]
    public class CartItemSession
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}