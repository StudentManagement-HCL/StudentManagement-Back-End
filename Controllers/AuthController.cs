using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // POST api/auth/admin/register
        [HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterRequest request)
        {
            var exists = await _context.AdminSignup.AnyAsync(a => a.Email == request.Email);
            if (exists) return BadRequest(new { message = "Email already registered." });

            var admin = new AdminSignup
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            _context.AdminSignup.Add(admin);
            await _context.SaveChangesAsync();

            var loginEntry = new AdminLogin
            {
                AdminId = admin.AdminId,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            _context.AdminLogin.Add(loginEntry);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Admin registered successfully." });
        }


        // POST api/auth/admin/login
        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginRequest request)
        {
            var admin = await _context.AdminSignup.FirstOrDefaultAsync(a => a.Email == request.Email);
            if (admin == null) return Unauthorized(new { message = "Invalid credentials." });

            var loginData = await _context.AdminLogin.FindAsync(admin.AdminId);
            if (loginData == null || !BCrypt.Net.BCrypt.Verify(request.Password, loginData.Password))
                return Unauthorized(new { message = "Invalid credentials." });

            var token = _jwtService.GenerateToken(admin.AdminId, admin.Email, "Admin", admin.FirstName + " " + admin.LastName);

            return Ok(new LoginResponse
            {
                Token = token,
                Role = "Admin",
                UserId = admin.AdminId,
                Name = admin.FirstName + " " + admin.LastName
            });
        }

        // POST api/auth/student/register
        [HttpPost("student/register")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRegisterRequest request)
        {
            var exists = await _context.StudentSignup.AnyAsync(s => s.Email == request.Email);
            if (exists) return BadRequest(new { message = "Email already registered." });

            var student = new StudentSignup
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                DoB = request.DoB,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            _context.StudentSignup.Add(student);
            await _context.SaveChangesAsync();

            var loginEntry = new StudentLogin
            {
                StudentId = student.StudentId,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            _context.StudentLogin.Add(loginEntry);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student registered successfully." });
        }
        // POST api/auth/student/login
        [HttpPost("student/login")]
        public async Task<IActionResult> StudentLogin([FromBody] LoginRequest request)
        {
            var student = await _context.StudentSignup.FirstOrDefaultAsync(s => s.Email == request.Email);
            if (student == null) return Unauthorized(new { message = "Invalid credentials." });

            var loginData = await _context.StudentLogin.FindAsync(student.StudentId);
            if (loginData == null || !BCrypt.Net.BCrypt.Verify(request.Password, loginData.Password))
                return Unauthorized(new { message = "Invalid credentials." });

            var token = _jwtService.GenerateToken(student.StudentId, student.Email, "Student", student.FirstName + " " + student.LastName);

            return Ok(new LoginResponse
            {
                Token = token,
                Role = "Student",
                UserId = student.StudentId,
                Name = student.FirstName + " " + student.LastName
            });
        }
    }
}
