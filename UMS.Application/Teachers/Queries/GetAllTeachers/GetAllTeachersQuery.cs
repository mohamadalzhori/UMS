using MediatR;

namespace UMS.Application.Teachers.Queries.GetAllTeachers
{
    public record GetAllTeachersQuery : IRequest<List<TeacherDto>>;
}
