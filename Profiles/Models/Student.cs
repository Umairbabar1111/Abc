using System;
using System.Collections.Generic;

namespace Profiles.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Class { get; set; }
        public string Rollno { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
