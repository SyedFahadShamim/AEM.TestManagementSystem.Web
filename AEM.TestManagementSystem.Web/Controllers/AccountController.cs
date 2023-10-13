using AEM.TestManagementSystem.Services.Interfaces;
using AEM.TestManagementSystem.Services.Models.DTO;
using AEM.TestManagementSystem.Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AEM.TestManagementSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IStudentService studentService;
        public AccountController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public IActionResult SignUp()
        {
            return View("~/Views/Account/SignUp.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid) 
                { 
                    return View(model); 
                }
                model.Role = "user";
                var result = await this.studentService.RegisterAsync(model);
                TempData["msg"] = result.Message;

                return RedirectToAction(nameof(SignUp));
            }
            catch (Exception)
            {

                throw;
            }

        }


        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await studentService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
    }
}
