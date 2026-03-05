using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using System.Security.Claims;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentEditController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentEditController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/studentedit/profile
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.email == email);

            if (student == null)
                return NotFound(new { message = "Student not found." });

            return Ok(new
            {
                id = student.id,
                name = student.name,
                email = student.email,
                age = student.age,
                department = student.department,
                dob = student.dob,
                phone = student.phone,
                city = student.city,
                state = student.state
            });
        }

        // PUT api/studentedit/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] StudentUpdateRequest request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.email == email);

            if (student == null)
                return NotFound(new { message = "Student not found." });

            // Only allow updating phone, city, state
            student.phone = request.Phone;
            student.city = request.City;
            student.state = request.State;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Profile updated successfully.",
                name = student.name,
                email = student.email,
                department = student.department,
                phone = student.phone,
                city = student.city,
                state = student.state
            });
        }
    }

    public class StudentUpdateRequest
    {
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}