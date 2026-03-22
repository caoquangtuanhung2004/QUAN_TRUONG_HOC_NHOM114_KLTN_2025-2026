using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;

using System.Web.Mvc;
namespace demomvc.Models
{
    public class GuiBangDiemViewModel
    {
        public string NamHoc { get; set; }
        public int HocKy { get; set; }

        public List<string> DanhSachNamHoc { get; set; }
        public List<int> DanhSachHocKy { get; set; }
    }



}