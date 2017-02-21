using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orders.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CurrentOrder()
        {
            ViewBag.Message = "Items in your current order";

            return View();
        }

        public ActionResult PreviousOrders()
        {
            ViewBag.Message = "Your submitted orders";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Made for ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Shoaib Khan";

            return View();
        }
    }
}