namespace HikingPlanAndRescue.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class HikingPlanAndRescueDbContext : IdentityDbContext<User>, IHikingPlanAndRescueDbContext
    {
        public HikingPlanAndRescueDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static HikingPlanAndRescueDbContext Create()
        {
            return new HikingPlanAndRescueDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //            .HasMany(u => u.Events)
            //            .WithMany(e => e.Participants)
            //            .Map(m =>
            //            {
            //                m.MapRightKey("EventId");
            //                m.MapLeftKey("ParticipantId");
            //                m.ToTable("EventsParticipants");
            //            });

            //modelBuilder.Entity<EventDay>()
            //            .HasMany(u => u.OptionalRoutes)
            //            .WithMany(e => e.EventDays)
            //            .Map(m =>
            //            {
            //                m.MapRightKey("EventDayId");
            //                m.MapLeftKey("OptionalRouteId");
            //                m.ToTable("EventDaysOptionalRoutes");
            //            });

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<User>().HasMany(c => c.Events).WithRequired(x => x.Creator).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Route>().HasRequired(c => c.StartPoint).WithMany().WillCascadeOnDelete(false);

            //modelBuilder.Entity<EventDay>().HasRequired(c => c.MainRoute).WithMany().WillCascadeOnDelete(false);

            //modelBuilder.Entity<Playlist>().HasMany(c => c.Videos).WithOptional(x => x.Playlist).WillCascadeOnDelete(true);

            //modelBuilder.Entity<Playlist>().HasMany(c => c.Ratings).WithRequired(x => x.Playlist).WillCascadeOnDelete(true);


            base.OnModelCreating(modelBuilder);
        }
    }
}