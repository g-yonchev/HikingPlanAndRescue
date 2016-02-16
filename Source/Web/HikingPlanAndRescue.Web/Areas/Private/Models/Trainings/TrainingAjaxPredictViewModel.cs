namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;
    using Infrastructure.Validation;

    public class TrainingAjaxPredictViewModel : IMap<Training>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "You must add a track.")]
        public TrackAjaxPredictViewModel Track { get; set; }

        public DateTime? EndDate { get; set; }

        public double? Water { get; set; }

        public double? Calories { get; set; }
    }
}
