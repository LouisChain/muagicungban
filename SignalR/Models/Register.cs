using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace muagicungban.Models
{
    public class Register
    {
        [ScaffoldColumn(false)]
        public DateTime RegisDate { get; set; }

        [Required(ErrorMessage="Tên đăng nhập bắt buộc phải có!!!")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage="Mật khẩu là không thể thiếu!!!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mật khẩu không khớp!!!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage="Phải có một địa chỉ để liên lạc !!!")]
        public string Address { get; set; }

        [Required(ErrorMessage="Vui lòng cho biết ngày sinh!!!")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage="Cần một cái tên để tiện hỏi thăm!!!")]
        public string Name { get; set; }

        [Required(ErrorMessage="Email là phải có!!!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email không hợp lệ.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage="Cần phải có điện thoại để liên lạc")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Hãy nhập mã xác thực!!!")]
        public string Captcha { get; set; }

    }
}