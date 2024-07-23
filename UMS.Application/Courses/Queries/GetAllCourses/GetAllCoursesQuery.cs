using MediatR;
using UMS.API.Controllers.Courses;

namespace UMS.Application.Courses.Queries.GetAllCourses
{
    public record GetAllCoursesQuery : IRequest<List<CourseDto>>;
}
