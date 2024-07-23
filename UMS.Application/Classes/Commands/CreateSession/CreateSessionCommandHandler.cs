using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Domain.Classes;
using UMS.Domain.Exceptions;
using UMS.Domain.Exceptions.Classes;
using UMS.Persistence;

namespace UMS.Application.Classes.Commands.CreateSession
{
    internal class CreateSessionCommandHandler(AppDbContext _context) : IRequestHandler<CreateSessionCommand, long>
    {
        public async Task<long> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var @class = _context.Classes.Include(x=> x.Sessions).FirstOrDefault(c => c.Id == request.ClassId);

            // TODO: Also check if overlap with other classes taught by the same teacher

            if (@class != null)
                throw new ClassNotFound(request.ClassId);

            var session = @class.AddSession(request.StartTime, request.EndTime);            

            await _context.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}
