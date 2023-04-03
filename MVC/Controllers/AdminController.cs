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
            return View();
        }
        public ActionResult LoginPage()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Password()
        {
            return View();
        }

        public ActionResult DHBan() { return View(); }

        public ActionResult PhieuNhapKho() { return View(); }
        public ActionResult PhieuXuatKho() { return View(); }
        public ActionResult BaoCaoTonKho() { return View(); }


    }
}