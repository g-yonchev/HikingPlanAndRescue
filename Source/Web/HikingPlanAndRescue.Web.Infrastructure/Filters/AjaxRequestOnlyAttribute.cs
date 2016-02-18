namespace HikingPlanAndRescue.Web.Infrastructure.Filters
{
    using System.Reflection;
    using System.Web.Mvc;

    public class AjaxRequestOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}