﻿namespace HikingPlanAndRescue.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return this.Redirect("/");
        }
    }
}