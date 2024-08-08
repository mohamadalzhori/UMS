using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Application.Caching;
using UMS.Persistence;

namespace UMS.Application.Students.Queries.GetAllStudents
{
    internal class GetAllStudentsQueryHandler(AppDbContext _constext, IMapper _mapper, ICacheService _cache) : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
    {
        private const string CacheKey = "AllStudents";
        async Task<List<StudentDto>> IRequestHandler<GetAllStudentsQuery, List<StudentDto>>.Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await _cache.GetAsync<List<StudentDto>>(CacheKey, cancellationToken);

            if (cachedData is not null && cachedData.Count() >0)
            {
                return cachedData;
            }
            
            var allStudents =  await _constext.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            await _cache.SetAsync(CacheKey, allStudents, TimeSpan.FromMinutes(30), cancellationToken);

            return allStudents;
        }
    }
}
