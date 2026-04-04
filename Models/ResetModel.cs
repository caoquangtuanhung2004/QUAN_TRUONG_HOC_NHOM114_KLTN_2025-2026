using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demomvc.Models
{
	public class ResetModel
	{
        public string Token { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Ít nhất 6 ký tự")]
        public string MatKhauMoi { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu không khớp")]
        public string XacNhanMatKhau { get; set; }
    }
}