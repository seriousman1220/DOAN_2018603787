using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return PartialView();
        }
        public ActionResult LoginPage()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return PartialView("Register");
        }
        public ActionResult Password()
        {
            return PartialView("Password");
        }

        public ActionResult DHBan() { return View(); }

        public ActionResult PhieuNhapKho() { return View(); }
        public ActionResult PhieuXuatKho() { return View(); }
        public ActionResult BaoCaoTonKho() { return View(); }


    }
}