using demomvc.App_Start;
using demomvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace demomvc.Controllers
{
    [RoleAuthorize(RolesRequired = "HieuTruong")]
    public class QuanLyThoiKhoaBieuController : Controller
    {
        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();

        // Trang đầu tiên
        public ActionResult Index()
        {
            var model = new TimLopViewModel();

            // Load Học kỳ
            model.ListHocKy = db.HocKy
                .ToList()
                .Select(x => new SelectListItem
                {
                    Value = x.HocKyID.ToString(),
                    Text = x.TenHocKy
                })
                .ToList();

            // Load Năm học
            model.ListNamHoc = db.NamHoc
                .ToList()
                .Select(x => new SelectListItem
                {
                    Value = x.NamHocID.ToString(),
                    Text = x.TenNamHoc
                })
                .ToList();

            model.ListLop = new List<LopHocViewModel>();

            return View(model);
        }

        // Khi bấm XEM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XemLop(TimLopViewModel model)
        {
            var vm = new TimLopViewModel();

            vm.ListHocKy = db.HocKy
             .OrderBy(x => x.TenHocKy)
             .ToList()
             .Select(x => new SelectListItem
             {
                 Value = x.HocKyID.ToString(),
                 Text = x.TenHocKy
             })
             .ToList();

            vm.ListNamHoc = db.NamHoc
                .OrderBy(x => x.TenNamHoc)
                .ToList()
                .Select(x => new SelectListItem
                {
                    Value = x.NamHocID.ToString(),
                    Text = x.TenNamHoc
                })
                .ToList();


            // kiểm tra model.NamHocID có giá trị hay không
            if (model.NamHocID != 0)
            {
                var list = db.LopHoc
              .Where(x => x.NienKhoa == model.NamHocID)
              .Select(x => new LopHocViewModel
              {
              
                  TenLop = x.TenLop
                 
              })
              .ToList();

                vm.ListLop = list;
            }
            else
            {
                vm.ListLop = new List<LopHocViewModel>();
            }

            vm.HocKyID = model.HocKyID;
            vm.NamHocID = model.NamHocID;

            return View("Index", vm);
        }

    }
}
