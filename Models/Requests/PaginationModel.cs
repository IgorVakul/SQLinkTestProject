using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class PaginationModel
    {
        [Required, Range(1, 9999999)]
        public int PageNumber { get; set; } = 1;

        [Required, Range(1, 250)]
        public int PageSize { get; set; } = 20;
    }
}
