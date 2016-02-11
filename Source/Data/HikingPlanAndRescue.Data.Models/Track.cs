namespace HikingPlanAndRescue.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Track : BaseModel<int>
    {
        public Track()
        {
        }

        public string Title { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        //[Required]
        //public string FilePath { get; set; }

        public double Length { get; set; }

        public double Ascent { get; set; }

        public double AscentLength { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }

        //public enum TrackCategory { get; set; }
    }
}
