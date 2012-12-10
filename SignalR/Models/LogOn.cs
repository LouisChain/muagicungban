using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace muagicungban.Models
{
    public class LogOn
    {
        [Required(ErrorMessage="Vui lòng nhập tên đăng nhập!!!")]
        public string Username { get; set; }

        [Required(ErrorMessage="Mật khẩu là không thể thiếu!!!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}