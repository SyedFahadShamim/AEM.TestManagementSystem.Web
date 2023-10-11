
using AEM.TestManagementSystem.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace TMS.DbContextClass
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUsers>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Students> students { get; set; }
    }
}
