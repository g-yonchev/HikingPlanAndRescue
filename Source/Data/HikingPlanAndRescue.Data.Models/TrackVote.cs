namespace HikingPlanAndRescue.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Models;

    public class TrackVote : IEntitiyWithCreator, IDeletableEntity, IAuditInfo
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int TrackId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Track Track { get; set; }

        public VoteType Vote { get; set; }

        public bool IsDeleted { get;  set; }

        public DateTime? DeletedOn { get;  set; }

        public DateTime CreatedOn { get;  set; }

        public DateTime? ModifiedOn { get;  set; }
    }
}