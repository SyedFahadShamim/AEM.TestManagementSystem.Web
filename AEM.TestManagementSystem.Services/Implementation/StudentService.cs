using AEM.TestManagementSystem.Repository.Interfaces;
using AEM.TestManagementSystem.Repository.Models;
using AEM.TestManagementSystem.Repository.Models.Domain;
using AEM.TestManagementSystem.Services.Interfaces;
using AEM.TestManagementSystem.Services.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AEM.TestManagementSystem.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IStudentRepository studentRepository;
        private readonly SignInManager<ApplicationUser> signInManager;
        public StudentService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IStudentRepository studentRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.studentRepository = studentRepository;
            this.signInManager = signInManager;

        }
        public async Task<Status> RegisterAsync(RegistrationModelDTO model)
        {
            try
            {
                var status = new Status();
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {
                    status.StatusCode = 0;
                    status.Message = "User already exist";
                    return status;
                }
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    status.StatusCode = 0;
                    status.Message = "User creation failed";
                    return status;
                }

                if (!await roleManager.RoleExistsAsync(model.Role))
                    await roleManager.CreateAsync(new IdentityRole(model.Role));


                if (await roleManager.RoleExistsAsync(model.Role))
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                }

                status.StatusCode = 1;
                status.Message = "You have registered successfully";
                return status;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<Status> LoginAsync(LoginModel model)
        {
            try
            {
                return await studentRepository.LoginAsync(model.Username, model.Password);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
