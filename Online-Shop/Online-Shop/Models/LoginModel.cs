using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Shop.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}