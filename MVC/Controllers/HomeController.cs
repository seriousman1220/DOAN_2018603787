using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult ShoppingCart()
        {
            return View();
        }

        public ActionResult Login()
        {
            return PartialView();
        }
        public ActionResult CheckOut()
        {
            return PartialView();
        }
        public ActionResult Contact()
        {
            return PartialView();
        }
        public ActionResult ProductDetails(string id)
        {
            return View();
        }
        public ActionResult ListProduct()
        {
            return View();
        }



    }
}