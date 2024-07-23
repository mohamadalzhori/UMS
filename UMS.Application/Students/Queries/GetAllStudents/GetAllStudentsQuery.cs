using MediatR;

namespace UMS.Application.Students.Queries.GetAllStudents
{
    public record GetAllStudentsQuery : IRequest<List<StudentDto>>;
}
