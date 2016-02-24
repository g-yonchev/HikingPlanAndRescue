namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System.Web.Mvc;

    public class HomeController : BasePrivateController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}