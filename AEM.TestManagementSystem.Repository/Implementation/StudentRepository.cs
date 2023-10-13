using AEM.TestManagementSystem.Repository.Interfaces;
using AEM.TestManagementSystem.Repository.Models;
using AEM.TestManagementSystem.Repository.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AEM.TestManagementSystem.Repository.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public StudentRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;

        }
        public async Task<Status> LoginAsync(string username, string password)
        {
			try
			{
                var status = new Status();
                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid username";
                    return status;
                }

                if (!await userManager.CheckPasswordAsync(user, password))
                {
                    status.StatusCode = 0;
                    status.Message = "Invalid Password";
                    return status;
                }

                var signInResult = await signInManager.PasswordSignInAsync(user, password, false, true);
                if (signInResult.Succeeded)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    status.StatusCode = 1;
                    status.Message = "Logged in successfully";
                }
                else if (signInResult.IsLockedOut)
                {
                    status.StatusCode = 0;
                    status.Message = "User is locked out";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on logging in";
                }

                return status;
            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
