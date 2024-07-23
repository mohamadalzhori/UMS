using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Teachers.Commands.CreateTeacher;
using UMS.Application.Teachers.Queries.GetAllTeachers;
using UMS.Domain.Shared;

namespace UMS.API.Controllers
{
    [ApiController]
    [Route("Teacher")]
    public class TeacherController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<long> CreateAsync([FromBody] CreateTeacherCommand command)
        {
            var teacherId = await _mediator.Send(command);

            return teacherId;
        }

        [HttpGet("GetAll")]
        public async Task<List<TeacherDto>> GetAllTeachers()
        {
            var query = new GetAllTeachersQuery();

            return await _mediator.Send(query);
        }


    }
}
