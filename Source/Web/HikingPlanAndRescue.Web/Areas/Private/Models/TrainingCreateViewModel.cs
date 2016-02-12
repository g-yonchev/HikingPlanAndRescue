namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;
    using Infrastructure.Validation;

    public class TrainingCreateViewModel : IMap<Training>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy hh:mm")]
        [CompareTrainingDateValidationAttribute(OtherProperty = "StartDate", ErrorMessage = "End date must be after start date")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Water { get; set; }

        [Required]
        public double Calories { get; set; }

        [Required(ErrorMessage = "You must add a track.")]
        public TrackCreateViewModel Track { get; set; }
    }
}