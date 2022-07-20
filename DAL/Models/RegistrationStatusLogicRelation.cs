using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class RegistrationStatusLogicRelation
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int NextStatusId { get; set; }
    }
}
