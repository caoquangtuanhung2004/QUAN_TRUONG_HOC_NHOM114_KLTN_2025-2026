using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demomvc.Models
{
	public class PhongHocViewModel
	{
        public int PhongHocID { get; set; }

        [Required(ErrorMessage = "Tên phòng không được để trống")]
        public string TenPhong { get; set; }

        [Required(ErrorMessage = "Loại phòng không được để trống")]
        public string LoaiPhong { get; set; }

        [Required(ErrorMessage = "Số lượng chỗ ngồi không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng chỗ ngồi phải > 0!")]
        public int SoLuongChoNgoi { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; }
        public string TenPhongKhongDau { get; set; }
    }
}