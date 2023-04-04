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
            return PartialView();
        }

        public ActionResult MainPage()
        {
            return PartialView("MainPage");
        }

        public ActionResult ShoppingCart()
        {
            return PartialView("ShoppingCart");
        }

       
        public ActionResult CheckOut()
        {
            return PartialView("CheckOut");
        }
        public ActionResult Contact()
        {
            return PartialView("Contact");
        }
        public ActionResult ProductDetails()
        {
            return PartialView("ProductDetails");
        }
        public ActionResult ListProduct()
        {
            return PartialView("ListProduct");
        }



    }
}