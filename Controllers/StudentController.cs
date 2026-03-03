using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public StudentController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("AddStudent")]
        public IActionResult AddStudent(Student student)
        {
            student.id = 0;  

            dbContext.Students.Add(student);
            dbContext.SaveChanges();

            return Ok(student);
        }
        [HttpGet("GetStudents")]
        public IActionResult GetStudents()
        {
            var students = dbContext.Students.ToList();
            return Ok(students);
        }
        [HttpPut("EditStudent/{id}")]
        public IActionResult EditStudent(int id, Student student)
        {
            var existingStudent = dbContext.Students.FirstOrDefault(s => s.id == id);

            if (existingStudent == null)
                return NotFound();

            existingStudent.name = student.name;
            existingStudent.age = student.age;
            existingStudent.department = student.department;
            existingStudent.email = student.email;
            existingStudent.phone = student.phone;
            existingStudent.city = student.city;
            existingStudent.state = student.state;
            existingStudent.dob = student.dob;

            dbContext.SaveChanges();

            return Ok(existingStudent);
        }

        [HttpDelete("DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = dbContext.Students.Find(id);

            if (student == null)
                return NotFound();

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();

            return Ok();
        }


    }
}
