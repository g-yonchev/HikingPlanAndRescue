namespace HikingPlanAndRescue.Web.Models.Tweet
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TweetInputModel
    {
        [Required]
        public string Content { get; set; }
    }
}
