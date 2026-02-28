using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class AdminSignup
    {
        [Key]
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}



