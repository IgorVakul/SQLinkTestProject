using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseRegistrations = new HashSet<CourseRegistration>();
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DurationInAcademicHours { get; set; }
        public string Lecturer { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public int LessonDurationInMinutes { get; set; }
        public string Place { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal CoursePrice { get; set; }

        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }        
    }
}
