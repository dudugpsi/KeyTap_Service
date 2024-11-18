using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeyTap_Service.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Uma grande descrição.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Pagina de contactos.";

            return View();
        }
    }
}