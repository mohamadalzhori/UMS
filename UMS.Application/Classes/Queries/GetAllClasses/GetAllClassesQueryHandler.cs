using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Application.Caching;
using UMS.Persistence;

namespace UMS.Application.Classes.Queries.GetAllClasses
{
    internal class GetAllClassesQueryHandler(AppDbContext _context, IMapper _mapper, ICacheService _cache)
        : IRequestHandler<GetAllClassesQuery, List<ClassDto>>
    {
        private const string CacheKey = "AllClasses";

        public async Task<List<ClassDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await _cache.GetAsync<List<ClassDto>>(CacheKey, cancellationToken);
            if (cachedData is not null)
            {
                return cachedData;
            } 
            
            var classes = await _context.Classes
                .ProjectTo<ClassDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // cache
            await _cache.SetAsync(CacheKey, classes, TimeSpan.FromMinutes(30), cancellationToken);

            return classes;
        }
    }
}