using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class PagingModel
    {
        public PagingModel(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        public int Skip { get; set; }

        [DefaultValue(10)]
        [Range(1, 999)]
        public int Take { get; set; }

    }
}
