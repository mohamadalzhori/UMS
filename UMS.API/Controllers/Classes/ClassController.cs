using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Classes.Commands.Enroll;
using UMS.Application.Classes.Commands.RegisterClass;
using UMS.Application.Classes.Queries.GetAllClasses;

namespace UMS.API.Controllers.Classes
{
    [ApiController]
    [Route("Class")]
    public class ClassController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<long> RegisterAsync([FromBody] RegisterClassRequest request)
        {
            var command = new RegisterClassCommand(
                request.TeacherId,
                request.CourseId
            );

            var classId = await _mediator.Send(command);

            return classId;
        }

        [HttpGet("GetAll")]
        public async Task<List<ClassDto>> GetAllClasses()
        {
            var query = new GetAllClassesQuery();

            return await _mediator.Send(query);
        }


        [HttpPost("Enroll")]
        public async Task<IActionResult> EnrollAsync([FromBody] EnrollRequest request)
        {
            var command = new EnrollCommand(
                request.StudentId,
                request.ClassId
            );

            await _mediator.Send(command);

            return Ok();
        }
    }
}
