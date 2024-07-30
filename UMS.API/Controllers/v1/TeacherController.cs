using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Teachers.Commands.CreateTeacher;
using UMS.Application.Teachers.Queries.GetAllTeachers;

namespace UMS.API.Controllers.v1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("v{version:apiVersion}/Teacher")]
    public class TeacherController(IMediator _mediator) : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<long> CreateAsync([FromBody] CreateTeacherCommand command)
        {
            var teacherId = await _mediator.Send(command);

            return teacherId;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAll")]
        public async Task<List<TeacherDto>> GetAllTeachers()
        {
            var query = new GetAllTeachersQuery();

            return await _mediator.Send(query);
        }


    }
}
