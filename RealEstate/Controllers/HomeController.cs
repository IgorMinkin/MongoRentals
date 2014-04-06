using MongoDB.Driver;
using RealEstate.Database;
using RealEstate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        protected readonly RealEstateContext Context;

        public HomeController()
        {
            Context = new RealEstateContext();
        }

        public ActionResult Index()
        {
            Context.Db.GetStats();
            return Json(Context.Db.Server.BuildInfo, JsonRequestBehavior.AllowGet);
       
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}