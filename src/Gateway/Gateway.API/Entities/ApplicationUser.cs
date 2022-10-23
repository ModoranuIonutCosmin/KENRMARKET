using Microsoft.AspNetCore.Identity;

namespace Gateway.API.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
