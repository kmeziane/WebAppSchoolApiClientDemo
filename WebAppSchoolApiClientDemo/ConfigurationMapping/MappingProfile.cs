using AutoMapper;
using WebAppSchoolApiClientDemo;
using WebAppSchoolApiClientDemo.Models;
using WebAppSchoolApiClientDemo.ViewModels;

namespace EventSWebAppSchoolApiClientDemoharing.ConfigurationMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseViewModel>()
                .ReverseMap();
            CreateMap<Student, StudentViewModel>()
                .ReverseMap();

            CreateMap<CourseDto, CourseViewModel>()
                .ReverseMap();
        }
    }
}
