using System;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string department { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public DateTime? dob { get; set; }
    }
}