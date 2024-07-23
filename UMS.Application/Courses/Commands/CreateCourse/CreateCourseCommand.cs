using MediatR;
using UMS.Domain.Courses;
using UMS.Domain.Shared;

namespace UMS.Application.Courses.Commands.CreateCourse
{
    public record CreateCourseCommand(
        string Name,
        int MaxStudentNumber,
        DateOnly StartDate,
        DateOnly EndDate) : IRequest<long>;
}
