namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrainingCreateViewModel : IMapFrom<Training>
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Water { get; set; }

        [Required]
        public double Calories { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage ="You must add a track.")]
        public int TrackId { get; set; }
    }
}
