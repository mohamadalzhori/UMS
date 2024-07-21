using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Classes;
using UMS.Persistence;

namespace UMS.Application.Classes.Commands.RegisterClass
{
    internal class RegisterClassCommandHandler(AppDbContext _context) : IRequestHandler<RegisterClassCommand, long>
    {
        public async Task<long> Handle(RegisterClassCommand request, CancellationToken cancellationToken)
        {
            var @class = Class.Register(request.TeacherId, request.CourseId);

            _context.Classes.Add(@class);

            await _context.SaveChangesAsync(cancellationToken);

            return @class.Id;
        }
    }
}
