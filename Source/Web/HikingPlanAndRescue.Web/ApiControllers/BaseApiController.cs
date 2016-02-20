namespace HikingPlanAndRescue.Web.ApiControllers
{
    using System.Web.Http;
    using AutoMapper;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;

    public class BaseApiController : ApiController
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}