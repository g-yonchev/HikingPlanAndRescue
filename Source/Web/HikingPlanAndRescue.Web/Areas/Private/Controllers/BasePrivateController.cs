namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System.Web.Mvc;
    using HikingPlanAndRescue.Web.Controllers;

    [Authorize]
    public class BasePrivateController : BaseController
    {
    }
}