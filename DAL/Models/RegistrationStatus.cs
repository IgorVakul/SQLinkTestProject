using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class RegistrationStatus
    {
        public RegistrationStatus()
        {
            CourseRegistrations = new HashSet<CourseRegistration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}
