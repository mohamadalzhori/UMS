using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.API.Controllers.Courses;
using UMS.Application.Classes.Queries.GetAllClasses;
using UMS.Application.Students.Queries.GetAllStudents;
using UMS.Application.Teachers.Queries.GetAllTeachers;
using UMS.Domain.Classes;
using UMS.Domain.Courses;
using UMS.Domain.Users;

namespace UMS.Application
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseDto>()
                 .ForMember(x => x.Name, dto => dto.MapFrom(x => x.Name.Value))
                 .ForMember(x => x.MaxStudentNumber, dto => dto.MapFrom(x => x.MaxStudentsNumber.Value));


            CreateMap<Teacher, TeacherDto>()
                .ForMember(x => x.Name, dto => dto.MapFrom(x => x.Name.Value))
                .ForMember(x => x.Email, dto => dto.MapFrom(x => x.Email.Value));

            CreateMap<Student, StudentDto>()
                .ForMember(x => x.Name, dto => dto.MapFrom(x => x.Name.Value))
                .ForMember(x => x.Email, dto => dto.MapFrom(x => x.Email.Value));

            CreateMap<Class, ClassDto>();
            
        }
    }
}
