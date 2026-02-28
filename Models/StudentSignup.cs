using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class StudentSignup
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DoB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
