using MediatR;
using UMS.Application.Students.Queries.GetAllStudents;

namespace UMS.Application.Classes.Queries.GetStudentsByClass;

public record GetStudentsByClassQuery(long ClassId) : IRequest<List<StudentDto>>;