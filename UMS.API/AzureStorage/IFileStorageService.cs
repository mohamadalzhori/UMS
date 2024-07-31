namespace UMS.API.AzureStorage;
public interface IFileStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string containerName, string blobName);
    Task<byte[]> DownloadFileAsync(string containerName, string blobName);    
}