using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    public class TracksController : Controller
    {
        // GET: Private/Tracks
        public ActionResult Index()
        {
            return View();
        }
    }
}