using AEM.TestManagementSystem.Repository.Models;

namespace AEM.TestManagementSystem.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<Status> LoginAsync(string username, string password);
    }
}
