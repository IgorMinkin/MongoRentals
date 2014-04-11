using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/
        [ChildActionOnly]
        public ActionResult Get()
        {
            return PartialView("GetSettings", new AppSettings());
        }
	}
}