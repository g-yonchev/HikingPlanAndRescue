﻿namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserViewModel : IMap<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GSM { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
