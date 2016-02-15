namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Trainings
{
    using System.Collections.Generic;
    using Users;

    public class TrainingFullEditViewModel
    {
        public IEnumerable<UserEditViewModel> Users { get; set; }

        public TrainingBasicEditViewModel Training { get; set; }
    }
}