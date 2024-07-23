using MediatR;
using UMS.Domain.Courses;
using UMS.Domain.Shared;

namespace UMS.Application.Teachers.Commands.CreateTeacher
{
    public record CreateTeacherCommand(
        string Name,
        string Email) : IRequest<long>;
}
