using MediatR;
using UMS.Domain.Shared;
using UMS.Domain.Users;
using UMS.Persistence;

namespace UMS.Application.Teachers.Commands.CreateTeacher
{
    internal class CreateTeacherCommandHandler(AppDbContext _context) : IRequestHandler<CreateTeacherCommand, long>
    {
        public async Task<long> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = new Teacher
            {
                Name = new Name(request.Name),
                Email = new Email(request.Email)
            };

            _context.Teachers.Add(teacher);

            await _context.SaveChangesAsync(cancellationToken);

            return teacher.Id;
        }
    }
}
