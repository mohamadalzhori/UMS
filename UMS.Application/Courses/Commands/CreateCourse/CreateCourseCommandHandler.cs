using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Courses;
using UMS.Domain.Shared;
using UMS.Persistence;

namespace UMS.Application.Courses.Commands.CreateCourse
{
    internal class CreateCourseCommandHandler(AppDbContext _context) : IRequestHandler<CreateCourseCommand, long>
    {
        public async Task<long> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Course.Create(new Name(request.Name), new MaxStudentNumber(request.MaxStudentNumber), request.StartDate, request.EndDate);

            _context.Courses.Add(course);

            await _context.SaveChangesAsync(cancellationToken);

            return course.Id;
        }
    }
}
