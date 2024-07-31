using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace UMS.API.AzureStorage;

public class AzureBlobStorageService :  IFileStorageService
{
    private readonly string _connectionString;

    public AzureBlobStorageService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string containerName, string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        }

        return blobClient.Uri.ToString();
    }

    public async Task<byte[]> DownloadFileAsync(string containerName, string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        if (await blobClient.ExistsAsync())
        {
            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (MemoryStream ms = new MemoryStream())
            {
                await download.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
        else
        {
            throw new FileNotFoundException($"Blob {blobName} not found in container {containerName}");
        }
    }
}