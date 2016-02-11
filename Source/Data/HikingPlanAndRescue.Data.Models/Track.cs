namespace HikingPlanAndRescue.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Track : BaseModel<int>
    {
        public Track()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Title { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        //[Required]
        //public string FilePath { get; set; }

        public double Length { get; set; }

        public double AvgAscentInclination { get; set; }

        public double TotalAscent { get; set; }

        //public enum TrackCategory { get; set; }
    }
}
