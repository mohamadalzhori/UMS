using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.API.AzureStorage;
using UMS.API.AzureStorage.Dto;
using UMS.Application.Students.Commands.AddPicture;
using UMS.Application.Students.Commands.CreateStudent;

namespace UMS.API.Controllers.v1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("v{version:apiVersion}/Student")]
    public class StudentController(IMediator _mediator, IWebHostEnvironment _env, IFileStorageService _fileStorageService) : ControllerBase
    {

        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<long> Create(CreateStudentCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "admin, student")]
        [HttpPost("UploadPictureLocal")]
        public async Task<IActionResult> UploadPictureLocal(long studentId, IFormFile picture)
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
        
        [Authorize(Roles = "admin, student")]
        [HttpPost("UploadPfpAzure")]
        public async Task<IActionResult> UploadPfpAzure([FromForm] UploadFileDto request)
        {
            if (request.file == null || request.file.Length == 0)
                return BadRequest("File is empty or not selected");

            var uri = await _fileStorageService.UploadFileAsync(request.file,request.containerName, request.blobName);
            return Ok(uri);
        }

        [Authorize(Roles = "admin, student")]
        [HttpGet("DownloadPfpAzure")]
        public async Task<IActionResult> DownloadPfpAzure([FromQuery] string containerName, [FromQuery] string blobName)
        {
            byte[] fileBytes = await _fileStorageService.DownloadFileAsync(containerName, blobName);
            return File(fileBytes, "application/octet-stream", blobName);
        } 
        
    }
}
