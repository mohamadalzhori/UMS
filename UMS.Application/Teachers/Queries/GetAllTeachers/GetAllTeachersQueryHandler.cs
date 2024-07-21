using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Persistence;

namespace UMS.Application.Teachers.Queries.GetAllTeachers
{
    internal class GetAllTeachersQueryHandler(AppDbContext _context, IMapper _mapper) : IRequestHandler<GetAllTeachersQuery, List<TeacherDto>>
    {
        public async Task<List<TeacherDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
        
            return await _context.Teachers
                .ProjectTo<TeacherDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        }
    }
}
