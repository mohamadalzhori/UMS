using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Persistence;

namespace UMS.Application.Classes.Commands.Enroll
{
    internal class EnrollCommandHandler(AppDbContext _context) : IRequestHandler<EnrollCommand>
    {
        public async Task Handle(EnrollCommand request, CancellationToken cancellationToken)
        {
            var @class = _context.Classes.Include(x => x.Course).FirstOrDefault(x => x.Id == request.ClassId);

            if (@class == null) 
                throw new NullReferenceException($"Class of id {request.ClassId} not found");

            var student = _context.Students.Find(request.StudentId);

            if (student == null)
                throw new NullReferenceException($"Student of id {request.StudentId} not found");

            @class.Enroll(student);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
