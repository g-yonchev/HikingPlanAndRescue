namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserEditViewModel : IMap<ApplicationUser>
    {
        [Required]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GSM { get; set; }
    }
}