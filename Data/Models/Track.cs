namespace HikingPlanAndRescue.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Track
    {
        public Track()
        {
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string FilePath { get; set; }

        public double Length { get; set; }

        public double AvgAscentInclination { get; set; }

        public double Ascent { get; set; }

        public double StressCoeff { get; set; }

        public double RoadsLength { get; set; }

        public double OffroadsLength { get; set; }

        public double TrailsLength { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}