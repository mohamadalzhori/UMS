using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UMS.Application.Students.Queries.GetAllStudents;
using UMS.Domain.Exceptions;
using UMS.Domain.Exceptions.Classes;
using UMS.Persistence;

namespace UMS.Application.Classes.Queries.GetStudentsByClass;

public class GetStudentsByClassQueryHandler(AppDbContext _context, IMapper _mapper) : IRequestHandler<GetStudentsByClassQuery, List<StudentDto>>
{
    public Task<List<StudentDto>> Handle(GetStudentsByClassQuery request, CancellationToken cancellationToken)
    {
        var classExists = _context.Classes.Any(x => x.Id == request.ClassId);
        if (!classExists)
            throw new ClassNotFound(request.ClassId);  
      
        return _context.ClassEnrollments
            .Include(x=> x.Student)
            .Where(x => x.ClassId == request.ClassId)
            .Select(x=> x.Student)
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}