namespace HikingPlanAndRescue.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            if (this.User.IsInRole(Common.AppConstants.AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { area = "admin" });
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home", new { area = "private" });
            }

            return this.View();
        }
    }
}