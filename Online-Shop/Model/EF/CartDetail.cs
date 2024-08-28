namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartDetail")]
    public partial class CartDetail
    {
        public int ID { get; set; }

        public int? CartID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}
