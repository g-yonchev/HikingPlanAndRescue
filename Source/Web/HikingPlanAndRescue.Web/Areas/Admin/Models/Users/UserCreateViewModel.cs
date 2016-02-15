namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Users
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserCreateViewModel : IMap<ApplicationUser>
    {
        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GSM { get; set; }
    }
}