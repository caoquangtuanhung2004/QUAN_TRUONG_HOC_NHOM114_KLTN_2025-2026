using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace demomvc.Models
{
	public class ThongBaoVM
	{
        [Required(ErrorMessage = "⚠ Vui lòng nhập tiêu đề")]
        public string TieuDe { get; set; }

       // [Required(ErrorMessage = "⚠ Vui lòng nhập nội dung")]
        [AllowHtml]
        public string NoiDung { get; set; }

        public bool GuiChoHocSinh { get; set; }
        public bool GuiChoGiaoVien { get; set; }
        public DateTime? NgayKetThucTB { get; set; }
    }
}