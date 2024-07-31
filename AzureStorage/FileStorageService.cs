using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureStorage;

public class FileStorageService
{
    const string connectionString =
        "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;";

    public static async Task<string> UploadFileAsync(UploadMessageDto uploadMessageDto)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(uploadMessageDto.ContainerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        BlobClient blobClient = containerClient.GetBlobClient(uploadMessageDto.BlobName);

        byte[] fileBytes = Convert.FromBase64String(uploadMessageDto.FileData);
        using (var stream = new MemoryStream(fileBytes))
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = uploadMessageDto.ContentType });
        }

        return blobClient.Uri.ToString();
    }

}