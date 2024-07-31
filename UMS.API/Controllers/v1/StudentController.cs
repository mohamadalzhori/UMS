using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMS.API.AzureStorage;
using UMS.API.AzureStorage.Dto;
using UMS.API.RabbitMQ;
using UMS.Application.Students.Commands.AddPicture;
using UMS.Application.Students.Commands.CreateStudent;

namespace UMS.API.Controllers.v1
{
    [ApiController]
    [ApiVersion(1)]
    [Route("v{version:apiVersion}/Student")]
    public class StudentController(IMediator _mediator, IWebHostEnvironment _env, IRabbitMqProducer _rabbitMqProducer, IFileStorageService _fileStorageService)
        : ControllerBase
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

            var path = Path.Combine(_env.WebRootPath, folder,
                Path.GetFileNameWithoutExtension(picture.FileName) + Guid.NewGuid().ToString() +
                Path.GetExtension(picture.FileName));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            var command = new AddPictureCommand(studentId, path);

            await _mediator.Send(command);

            return Ok(path);
        }

        // [Authorize(Roles = "admin, student")]
        [HttpPost("UploadPfpAzure")]
        public async Task<IActionResult> UploadPfpAzure([FromForm] UploadFileDto request,
            CancellationToken cancellationToken)
        {
            if (request.file == null || request.file.Length == 0)
                return BadRequest("File is empty or not selected");

            // Read the file into a byte array
            byte[] fileData;
            using (var ms = new MemoryStream())
            {
                await request.file.CopyToAsync(ms);
                fileData = ms.ToArray();
            }

            // Create a new DTO with the file data as a base64 string
            var fileMessageDto = new UploadMessageDto
            {
                ContainerName = request.containerName,
                BlobName = request.blobName,
                FileData = Convert.ToBase64String(fileData),
                ContentType = request.file.ContentType
            };

           _rabbitMqProducer.PublishFile(fileMessageDto); 
            
            return Ok();

        }

        // [Authorize(Roles = "admin, student")]
        [HttpGet("DownloadPfpAzure")]
        public async Task<IActionResult> DownloadPfpAzure([FromQuery] string containerName, [FromQuery] string blobName)
        {
            byte[] fileBytes = await _fileStorageService.DownloadFileAsync(containerName, blobName);

            return File(fileBytes, "application/octet-stream", blobName);

            return Ok();
        }

    }
}