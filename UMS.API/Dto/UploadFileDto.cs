namespace UMS.API.AzureStorage.Dto;

public class UploadFileDto
{
    public IFormFile file {get; set;}
    public string containerName {get; set;}
    public string blobName {get; set;}
}