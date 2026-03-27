using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace demomvc.Models
{
	public class CapNhatLopViewModel
	{
        public int LopHocID { get; set; }

        public string TenLop { get; set; }

        public int? GiaoVienChuNhiem { get; set; }

        public string TenKhoi { get; set; } 
        public string TrangThaiNamHoc { get; set; } 

        public int NamHocID { get; set; } 

        public List<SelectListItem> ListGiaoVien { get; set; }
    }
}