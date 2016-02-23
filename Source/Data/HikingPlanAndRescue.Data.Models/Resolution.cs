namespace HikingPlanAndRescue.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;

    public class Resolution : BaseModel<int>, IEntitiyWithCreator
    {
        public int TrainingId { get; set; }

        public virtual Training Training { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Comment { get; set; }
    }
}
