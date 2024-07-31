namespace UMS.API.AzureStorage;
public interface IFileStorageService
{
    Task<byte[]> DownloadFileAsync(string containerName, string blobName);    
}