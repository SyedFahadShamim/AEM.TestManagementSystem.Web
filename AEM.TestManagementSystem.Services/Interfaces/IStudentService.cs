using AEM.TestManagementSystem.Repository.Models;
using AEM.TestManagementSystem.Services.Models.DTO;

namespace AEM.TestManagementSystem.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Status> RegisterAsync(RegistrationModelDTO model);
        Task<Status> LoginAsync(LoginModel model);
    }
}
