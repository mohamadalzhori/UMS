using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Domain.Exceptions.Classes;
using UMS.Domain.Exceptions.Students;
using UMS.Persistence;

namespace UMS.Application.Classes.Commands.Enroll
{
    internal class EnrollCommandHandler(AppDbContext _context) : IRequestHandler<EnrollCommand>
    {
        public async Task Handle(EnrollCommand request, CancellationToken cancellationToken)
        {
            var @class = _context.Classes.Include(x => x.Course).FirstOrDefault(x => x.Id == request.ClassId);

            if (@class == null)
                throw new ClassNotFound(request.ClassId);

            var student = _context.Students.Find(request.StudentId);

            if (student == null)
                throw new StudentNotFound(request.StudentId);

            @class.Enroll(student);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
