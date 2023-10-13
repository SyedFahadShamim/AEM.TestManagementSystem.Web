using Microsoft.AspNetCore.Identity;

namespace AEM.TestManagementSystem.Repository.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
