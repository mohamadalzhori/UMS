namespace AzureStorage;
public class UploadMessageDto
{
    public string ContainerName { get; set; }
    public string BlobName { get; set; }
    public string FileData { get; set; }
    public string ContentType { get; set; }
}