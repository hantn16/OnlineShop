using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời nhập Password")]
        public string Password { get; set; }
        [Required()]
        public bool RememberMe { get; set; }
    }
}