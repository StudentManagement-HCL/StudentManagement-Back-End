using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class StudentLogin
    {
        [Key]
        public int StudentId { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        public DateTime dob { get; set; }
    }
}
