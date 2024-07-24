using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UMS.Application.Students.Commands.AddPicture;
using UMS.Application.Students.Commands.CreateStudent;
using UMS.Application.Students.Queries.GetAllStudents;

namespace UMS.API.Controllers.v1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("Student")]
    public class StudentController(IMediator _mediator, IWebHostEnvironment _env) : ControllerBase
    {

        [HttpPost("Create")]
        public async Task<long> Create(CreateStudentCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("UploadPicture")]
        public async Task<IActionResult> UploadPicture(long studentId, IFormFile picture)
        {
            var folder = "Pictures/";

            //  fileName + guid + extension

            var path = Path.Combine(_env.WebRootPath, folder, Path.GetFileNameWithoutExtension(picture.FileName) + Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            var command = new AddPictureCommand(studentId, path);

            await _mediator.Send(command);

            return Ok(path);
        }
    }
}
