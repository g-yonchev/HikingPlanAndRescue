namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrackCreateViewModel : IMapTo<Track>
    {
        [Display(Name = "Track title")]
        public string Title { get; set; }

        public double Length { get; set; }

        public double Ascent { get; set; }

        public double AscentLength { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
