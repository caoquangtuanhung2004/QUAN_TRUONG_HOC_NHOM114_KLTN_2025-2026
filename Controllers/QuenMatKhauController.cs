using demomvc.App_Start;
using demomvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace demomvc.Controllers
{
    public class QuenMatKhauController : Controller
    {
        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        // GET: QuenMatKhau

        //nhap email
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //guiwr yeeu caauf reset

        [HttpPost]
        public ActionResult Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.error = "vui long nhap email";
                return View();
            }

            var user = db.NguoiDung.FirstOrDefault(x => x.Email == email);

            if(user != null)
            {
                string token = GenerateToken();

                user.ResetToken = token;
                user.ResetTokenExpiry = DateTime.Now.AddMinutes(15);

                db.SaveChanges();
                SendResetEmail(user.Email, token);
            }

            ViewBag.message = "Neeu email ton tai, chungs toi da gui link dat laij mat khau qua email cua ban";
            return View();
        }

        // mo link reset
        public ActionResult DatLaiMatKhau(string token)
        {
            var user = db.NguoiDung.FirstOrDefault(x => x.ResetToken == token && x.ResetTokenExpiry > DateTime.Now);

            if(user == null)
            {
                return View("TokenKhongHopLe");
            }
            return View(new ResetModel { Token = token });
        }

        //Luu mat khau
        [HttpPost]
        public ActionResult DatLaiMatKhau(ResetModel model)
        {
            var user = db.NguoiDung.FirstOrDefault(x => x.ResetToken == model.Token &&
                                                    x.ResetTokenExpiry > DateTime.Now);

            if (user == null)
            {
                return View("TokenKhongHopLe");

            }
            if(model.MatKhauMoi != model.XacNhanMatKhau)
            {
                ModelState.AddModelError("", "Mat khau khong khop");
                return View(model);
            }

            user.MatKhau = PasswordHelper.HashPassword(model.MatKhauMoi);

            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            db.SaveChanges();

            TempData["Success"] = "Dat laij mat khau";
            return RedirectToAction("Index", "DangNhap");

        }

        //tao token

        private string generateToken()
        {
            byte[] data = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }
            return Convert.ToBase64String(data);
        }


        //guiwr email
        private void SendResetEmail(string toEmail, string token)
        {
            string resetLink = Url.Action(
                    "DatLaiMatKhau",
                    "QuenMatKhau",
                    new {token = token},
                    protocol: Request.Url.Scheme
                );

            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.Subject = "Dat lai mat khau";
            mail.Body = $"Nhan vao link sau de dat lai mat khau:<br/><a href='{resetLink}'>Đặt lại mật khẩu</a>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("yourgmail.com", "app_password");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }



    }
}