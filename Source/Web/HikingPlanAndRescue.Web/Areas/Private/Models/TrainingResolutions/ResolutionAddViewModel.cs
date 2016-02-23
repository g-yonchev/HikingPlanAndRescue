namespace HikingPlanAndRescue.Web.Areas.Private.Models.TrainingResolutions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ResolutionAddViewModel : IMap<Resolution>
    {
        public int TrainingId { get; set; }

        public string Comment { get; set; }
    }
}
