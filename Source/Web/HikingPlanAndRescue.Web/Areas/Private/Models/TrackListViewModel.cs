namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TrackListViewModel
    {
        [Required]
        public string Title { get; set; }

        public string UserId { get; set; }

        [Required]
        public double Length { get; set; }

        [Required]
        public double Ascent { get; set; }

        [Required]
        public double AscentLength { get; set; }
    }
}
