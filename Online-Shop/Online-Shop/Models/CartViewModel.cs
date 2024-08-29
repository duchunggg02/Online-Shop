using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Shop.Models
{
    public class CartViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => ProductPrice * Quantity;
    }
}