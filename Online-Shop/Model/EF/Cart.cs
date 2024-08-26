namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? UserID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Status { get; set; }

        public int Quantity { get; set; }

        public int ProductID { get; set; }
    }
}
