using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.API.Controllers.Courses;
using UMS.Application.Classes.Commands.CreateSession;
using UMS.Application.Courses.Commands.CreateCourse;
using UMS.Application.Courses.Queries.GetAllCourses;

namespace UMS.API.Controllers.v1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("v{version:apiVersion}/Course")]
    public class CourseController(IMediator _mediator) : ControllerBase
    {

        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<long> CreateAsync([FromBody] CreateCourseCommand command)
        {

            var courseId = await _mediator.Send(command);

            return courseId;
        }

        [HttpGet("GetAll")]
        public async Task<List<CourseDto>> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();

            return await _mediator.Send(query);
        }

        [Authorize(Roles = "admin, teacher")]
        [HttpPost("AddSession")]
        public async Task<long> AddSession([FromBody] CreateSessionCommand command)
        {
            var sessionId = await _mediator.Send(command);

            return sessionId;
        }
    }
}
