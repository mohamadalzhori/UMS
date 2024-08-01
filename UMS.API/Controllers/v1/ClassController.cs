using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Classes.Commands.Enroll;
using UMS.Application.Classes.Commands.RegisterClass;
using UMS.Application.Classes.Queries.GetAllClasses;
using UMS.Application.Classes.Queries.GetStudentsByClass;
using UMS.Application.Students.Queries.GetAllStudents;

namespace UMS.API.Controllers.v1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("v{version:apiVersion}/Class")]
    public class ClassController(IMediator _mediator) : ControllerBase
    {
        // [Authorize(Roles = "admin")]
        [HttpPost("Register")]
        public async Task<long> Register([FromBody] RegisterClassCommand command)
        {
            var classId = await _mediator.Send(command);

            return classId;
        }

        // [Authorize]
        [HttpGet("GetAll")]
        public async Task<List<ClassDto>> GetAllClasses()
        {
            var query = new GetAllClassesQuery();

            return await _mediator.Send(query);
        }


        [Authorize(Roles = "student")]
        [HttpPost("Enroll")]
        public async Task<IActionResult> Enroll([FromBody] EnrollCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [Authorize(Roles = "admin, teacher")]
        [HttpGet("GetStudents")]
        public async Task<List<StudentDto>> GetStudents([FromQuery] GetStudentsByClassQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
