using UMS.API.AzureStorage.Dto;
using UMS.API.Controllers.v1;

namespace UMS.API.RabbitMQ;

public interface IRabbitMqProducer
{
   public void PublishFile(UploadMessageDto uploadMessageDto); 
}