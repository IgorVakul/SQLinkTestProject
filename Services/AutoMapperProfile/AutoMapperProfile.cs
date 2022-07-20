using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Requests.AddUpdateCourseRequest, Models.DTO.CourseDTO>()
               .ForMember(dest => dest.StartHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartHour)))
               .ForMember(dest => dest.EndHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndHour)))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));

            CreateMap<Models.Requests.AddUpdateCourseRequest, DAL.Models.Course>()
              .ForMember(dest => dest.StartHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.StartHour)))
              .ForMember(dest => dest.EndHour, opt => opt.MapFrom(src => TimeSpan.Parse(src.EndHour)))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));

            CreateMap<DAL.Models.Course, Models.DTO.CourseDTO>();
            CreateMap<Models.DTO.CourseDTO, DAL.Models.Course>();
        }

    }
}
