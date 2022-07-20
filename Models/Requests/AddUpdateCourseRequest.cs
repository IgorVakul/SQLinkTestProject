using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class AddUpdateCourseRequest
    {

        [Required]
        public int ID { get; set; }

        [Required, StringLength(250, MinimumLength = 2)]
        public string CourseName { get; set; }

        [Required]
        public string CourseDescription { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int DurationInAcademicHours { get; set; }

        [Required]
        public string Lecturer { get; set; }

        [Required]
        public string StartHour { get; set; }

        [Required]
        public string EndHour { get; set; }

        [Required]
        public int LessonDurationInMinutes { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public decimal CoursePrice { get; set; }



    }
}
