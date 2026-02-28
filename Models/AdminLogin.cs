using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class AdminLogin
    {
        [Key]
        public int AdminId { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
