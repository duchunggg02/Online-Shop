﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CateName { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public decimal? PromotionPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
