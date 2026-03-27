using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace demomvc.Models
{
	public class PhanCongChuNhiemVM
	{
        public int LopHocID { get; set; }
        public string TenLop { get; set; }
        public int? GiaoVienChuNhiemID { get; set; }
        public string TenGiaoVien { get; set; }
        public int? NamHocID { get; set; }
        public List<SelectListItem> ListGiaoVien { get; set; } // dropdown GV
        public string TenKhoi { get; set; }
    }
}