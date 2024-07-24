using MediatR;

namespace UMS.Application.Students.Commands.AddPicture;

public record AddPictureCommand(long StudentId,string Path) : IRequest;