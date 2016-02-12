namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrackCreateViewModel : IMap<Track>
    {
        [Display(Name = "Track title")]
        public string Title { get; set; }

        [Display(Name = "Length (km)")]
        public double Length { get; set; }

        [Display(Name = "Ascent (m)")]
        public double Ascent { get; set; }

        [Display(Name = "Ascent length (km)")]
        public double AscentLength { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
