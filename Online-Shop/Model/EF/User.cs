namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [StringLength(10)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [StringLength(250)]
        public string Image { get; set; }
    }
}
