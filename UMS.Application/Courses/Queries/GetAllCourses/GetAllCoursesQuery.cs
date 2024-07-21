using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.API.Controllers.Courses;
using UMS.Domain.Courses;

namespace UMS.Application.Courses.Queries.GetAllCourses
{
    public record GetAllCoursesQuery : IRequest<List<CourseDto>>;
}
