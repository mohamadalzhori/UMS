using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Application.Classes.Commands.CreateSession;

namespace UMS.API.Controllers.Sessions
{
    [ApiController]
    [Route("Session")]
    public class SessionController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<long> CreateAsync([FromBody] CreateSessionRequest request)
        {
            var command = new CreateSessionCommand(
                request.StartTime,
                request.EndTime,
                request.ClassId
            );

            var sessionId = await _mediator.Send(command);

            return sessionId;
        }
    }
}
