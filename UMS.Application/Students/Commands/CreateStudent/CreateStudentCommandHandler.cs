using MediatR;
using UMS.Domain.Users;
using UMS.Persistence;

namespace UMS.Application.Students.Commands.CreateStudent
{
    internal class CreateStudentCommandHandler(AppDbContext _context) : IRequestHandler<CreateStudentCommand, long>
    {
        public async Task<long> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
           var student = new Student { Email = request.Email, Name = request.Name }; 

            _context.Students.Add(student);

            await _context.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }
}
