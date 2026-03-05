//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StudentManagementSystem.Data;
//using System.Security.Claims;

//namespace StudentManagementSystem.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Student")]
//    public class StudentEditController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public StudentEditController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET api/studentedit/profile
//        [HttpGet("profile")]
//        public async Task<IActionResult> GetProfile()
//        {
//            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (studentIdClaim == null) return Unauthorized();

//            int studentId = int.Parse(studentIdClaim);

//            var student = await _context.StudentSignup
//                .FirstOrDefaultAsync(s => s.StudentId == studentId);

//            if (student == null) return NotFound(new { message = "Student not found." });

//            // Find matching student record by email
//            var studentDetails = await _context.Students
//                .FirstOrDefaultAsync(s => s.email == student.Email);

//            return Ok(new
//            {
//                firstName = student.FirstName,
//                lastName = student.LastName,
//                email = student.Email,
//                phone = studentDetails?.phone ?? student.PhoneNumber,
//                city = studentDetails?.city ?? "",
//                state = studentDetails?.state ?? ""
//            });
//        }

//        // PUT api/studentedit/update
//        [HttpPut("update")]
//        public async Task<IActionResult> UpdateProfile([FromBody] StudentUpdateRequest request)
//        {
//            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (studentIdClaim == null) return Unauthorized();

//            int studentId = int.Parse(studentIdClaim);

//            var studentSignup = await _context.StudentSignup
//                .FirstOrDefaultAsync(s => s.StudentId == studentId);

//            if (studentSignup == null) return NotFound(new { message = "Student not found." });

//            // Update phone in StudentSignup table
//            studentSignup.PhoneNumber = request.Phone;

//            // Find or update in Students table
//            var studentDetails = await _context.Students
//                .FirstOrDefaultAsync(s => s.email == studentSignup.Email);

//            if (studentDetails != null)
//            {
//                studentDetails.phone = request.Phone;
//                studentDetails.city = request.City;
//                studentDetails.state = request.State;
//            }

//            await _context.SaveChangesAsync();

//            return Ok(new
//            {
//                message = "Profile updated successfully.",
//                phone = request.Phone,
//                city = request.City,
//                state = request.State,
//                firstName = studentSignup.FirstName,
//                lastName = studentSignup.LastName,
//                email = studentSignup.Email
//            });
//        }
//    }

//    public class StudentUpdateRequest
//    {
//        public string Phone { get; set; }
//        public string City { get; set; }
//        public string State { get; set; }
//    }
//}