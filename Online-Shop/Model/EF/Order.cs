namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CustomerID { get; set; }

        public int? Status { get; set; }

        [StringLength(100)]
        public string PaymentMethod { get; set; }

        public virtual User User { get; set; }
    }
}
