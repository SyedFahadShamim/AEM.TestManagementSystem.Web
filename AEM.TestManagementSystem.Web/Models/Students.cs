using System.ComponentModel.DataAnnotations;

namespace AEM.TestManagementSystem.Web.Models
{
    public class Students
    {
        // Properties
        [Key]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Constructor
        public Students()
        {

            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
        }
    }

}
