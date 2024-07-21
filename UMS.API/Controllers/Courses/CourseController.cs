using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Courses.Commands.CreateCourse;
using UMS.Application.Courses.Queries.GetAllCourses;
using UMS.Domain.Courses;
using UMS.Domain.Shared;

namespace UMS.API.Controllers.Courses
{
    [ApiController]
    [Route("Course")]
    public class CourseController(IMediator _mediator) : ControllerBase
    {

        [HttpPost("Create")]
        public async Task<long> CreateAsync([FromBody] CreateCourseRequest request)
        {
            
            var command = new CreateCourseCommand(
                new Name(request.Name),
                new MaxStudentNumber(request.MaxStudentNumber),
                request.StartDate,
                request.EndDate
            );

            var courseId = await _mediator.Send(command);

            return courseId;
        }

        [HttpGet("GetAll")]
        public async Task<List<CourseDto>> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();

            return await _mediator.Send(query);
        }
        
    }
}
