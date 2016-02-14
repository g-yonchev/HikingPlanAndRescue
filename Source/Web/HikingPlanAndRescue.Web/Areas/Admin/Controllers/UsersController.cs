namespace HikingPlanAndRescue.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using HikingPlanAndRescue.Web.Controllers;

    public class UsersController : BaseController
    {
        public UsersController()
        {
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}