using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.API.Controllers.Courses;
using UMS.Domain.Courses;
using UMS.Persistence;

namespace UMS.Application.Courses.Queries.GetAllCourses
{
    internal class GetAllCoursesQueryHandler(AppDbContext _context, IMapper _mapper) : IRequestHandler<GetAllCoursesQuery, List<CourseDto>>
    {
        public async Task<List<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Courses
                .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
