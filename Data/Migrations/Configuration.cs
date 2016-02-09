namespace HikingPlanAndRescue.Data.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	using System.Linq;

	using Microsoft.AspNet.Identity;

	using HikingPlanAndRescue.Data;

	public sealed class Configuration : DbMigrationsConfiguration<HikingPlanAndRescueDbContext>
    {
        public Configuration()
        {
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}
	}
}
