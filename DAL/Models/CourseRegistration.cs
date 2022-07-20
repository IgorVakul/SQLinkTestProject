using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class CourseRegistration
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual RegistrationStatus Status { get; set; }
        public virtual Student Student { get; set; }
    }
}
