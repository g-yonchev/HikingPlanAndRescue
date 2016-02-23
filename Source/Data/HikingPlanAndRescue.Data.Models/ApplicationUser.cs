namespace HikingPlanAndRescue.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IDeletableEntity, IAuditInfo
    {
        public ApplicationUser()
        {
            this.Tracks = new HashSet<Track>();
            this.Trainings = new HashSet<Training>();
            this.TrackVotes = new HashSet<TrackVote>();
            this.CreatedOn = DateTime.Now;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GSM { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }

        public virtual ICollection<TrackVote> TrackVotes { get; set; }

        public virtual ICollection<Resolution> Resolutions { get; set; }

        public bool IsDeleted { get;  set; }

        public DateTime? DeletedOn { get;  set; }

        public DateTime CreatedOn { get;  set; }

        public DateTime? ModifiedOn { get;  set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
