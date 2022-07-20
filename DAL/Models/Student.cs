using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Student
    {
        public Student()
        {
            CourseRegistrations = new HashSet<CourseRegistration>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}
