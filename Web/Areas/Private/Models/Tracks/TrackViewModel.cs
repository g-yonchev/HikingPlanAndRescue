namespace HikingPlanAndRescue.Web.Areas.Private.Models.Tracks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TrackViewModel
    {
        public string Name { get; set; }

        public double Length { get; set; }

        public double StressCoeff { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
