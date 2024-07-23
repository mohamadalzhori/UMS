using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UMS.Application.Students.Commands.CreateStudent;
using UMS.Application.Students.Queries.GetAllStudents;

namespace UMS.API.Controllers.v2
{
    [ApiController]
    [ApiVersion(2)]
    [Route("v{version:apiVersion}/Student")]
    public class StudentController(IMediator _mediator) : ODataController
    {

        [HttpPost("Create")]
        public async Task<long> Create(CreateStudentCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetAll")]
        [EnableQuery]
        public async Task<List<StudentDto>> GetAll()
        {
            var query = new GetAllStudentsQuery();

            return await _mediator.Send(query);
        }

    }
}
