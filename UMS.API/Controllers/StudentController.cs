using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UMS.Application.Students.Commands.CreateStudent;
using UMS.Application.Students.Queries.GetAllStudents;
using UMS.Domain.Shared;
using UMS.Domain.Users;

namespace UMS.API.Controllers
{
    [ApiController]
    [Route("Student")]
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
