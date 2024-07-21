using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UMS.Application.Students.CreateStudent;
using UMS.Application.Students.GetAllStudents;
using UMS.Domain.Shared;
using UMS.Domain.Users;

namespace UMS.API.Controllers.Students
{
    [ApiController]
    [Route("Student")]
    public class StudentController(IMediator _mediator) : ODataController
    {

        [HttpPost("Create")]
        public async Task<long> Create(CreateStudentRequest request)
        {
            var command = new CreateStudentCommand(
                new Name(request.Name),
                new Email(request.Email)
            );

            return await _mediator.Send(command);
        }

        [HttpGet]
        [EnableQuery]
        public async Task<List<Student>> Get()
        {
            var query = new GetAllStudentsQuery();
            
            return await _mediator.Send(query);
        }

    }
}
