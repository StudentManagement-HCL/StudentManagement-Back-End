using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProfile : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public AdminProfile(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("profile/{id}")]
        public IActionResult GetAdminProfile(int id)
        {
            var admin = dbContext.AdminSignup
                        .FirstOrDefault(a => a.AdminId == id);

            if (admin == null)
                return NotFound();

            return Ok(new
            {
                adminId = admin.AdminId,
                firstName = admin.FirstName,
                lastName = admin.LastName,
                email = admin.Email,
                phoneNumber = admin.PhoneNumber
            });
        }

        [HttpPut("update")]
        public IActionResult UpdateAdminProfile([FromBody] AdminSignup updatedAdmin)
        {
            var admin = dbContext.AdminSignup
                        .FirstOrDefault(a => a.AdminId == updatedAdmin.AdminId);

            if (admin == null)
                return NotFound("Admin not found");

            admin.FirstName = updatedAdmin.FirstName;
            admin.LastName = updatedAdmin.LastName;
            admin.Email = updatedAdmin.Email;
            admin.PhoneNumber = updatedAdmin.PhoneNumber;

            dbContext.SaveChanges();

            return Ok("Profile Updated Successfully");
        }


    }
}
