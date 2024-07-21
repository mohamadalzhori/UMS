using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Domain.Courses;
using UMS.Persistence;

namespace UMS.Application.Courses.Commands.CreateCourse
{
    internal class CreateCourceCommandHandler(AppDbContext _context) : IRequestHandler<CreateCourseCommand, long>
    {
        public async Task<long> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = Course.Create(request.Name, request.MaxStudentNumber, request.StartDate, request.EndDate);

            _context.Courses.Add(course);

            await _context.SaveChangesAsync(cancellationToken);

            return course.Id;
        }
    }
}
