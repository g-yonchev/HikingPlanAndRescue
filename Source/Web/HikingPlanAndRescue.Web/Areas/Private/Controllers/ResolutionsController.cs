namespace HikingPlanAndRescue.Web.Areas.Private.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using HikingPlanAndRescue.Web.Controllers;
    using Infrastructure.CustomExceptions;
    using Microsoft.AspNet.Identity;
    using Models.TrainingResolutions;
    using Services.Data.Contracts;

    public class ResolutionsController : BaseController
    {
        private IResolutionsService resolutions;
        private IUsersService users;

        public ResolutionsController(IResolutionsService resolutions, IUsersService users)
        {
            this.resolutions = resolutions;
            this.users = users;
        }

        // GET: Private/Resolutions
        public ActionResult Add(ResolutionAddViewModel model)
        {
            var resolution = Mapper.Map<Resolution>(model);
            var userId = User.Identity.GetUserId();
            resolution.UserId = userId;
            try
            {
                resolutions.AddResolution(resolution);
                resolutions.Save();
            }
            catch (CustomServiceOperationException e)
            {
                return Json(new { error = e.Message });
            }

            resolution.User = users.GetById(userId);
            var resultViewModel = Mapper.Map<ResolutionViewModel>(resolution);
            return PartialView("_ResolutionListItem", resultViewModel);
        }
    }
}