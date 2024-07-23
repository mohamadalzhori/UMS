using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Persistence;

namespace UMS.Application.Students.Queries.GetAllStudents
{
    internal class GetAllStudentsQueryHandler(AppDbContext _constext, IMapper _mapper) : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
    {

        async Task<List<StudentDto>> IRequestHandler<GetAllStudentsQuery, List<StudentDto>>.Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _constext.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
