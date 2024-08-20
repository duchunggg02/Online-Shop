using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Shop.Models
{
    public class RegisterModel
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Required]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Họ")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Tên")]
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public string Image { get; set; }
    }
}