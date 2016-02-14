﻿namespace HikingPlanAndRescue.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserPublicViewModel : IMap<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}