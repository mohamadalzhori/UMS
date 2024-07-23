using MediatR;
using UMS.Domain.Shared;

namespace UMS.Application.Students.Commands.CreateStudent
{
    public record CreateStudentCommand(Name Name, Email Email) : IRequest<long>;
}
