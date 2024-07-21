using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Persistence;

namespace UMS.Application.Classes.Queries.GetAllClasses
{
    internal class GetAllClassesQueryHandler(AppDbContext _context, IMapper _mapper) : IRequestHandler<GetAllClassesQuery, List<ClassDto>>
    {
        public async Task<List<ClassDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            return await  _context.Classes
                .ProjectTo<ClassDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
