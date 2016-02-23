namespace HikingPlanAndRescue.Web.Areas.Private.Models.TrainingResolutions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;
    using ViewModels.Users;

    public class ResolutionViewModel : IMap<Resolution>
    {
        public string UserId { get; set; }

        public UserPublicViewModel User { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
