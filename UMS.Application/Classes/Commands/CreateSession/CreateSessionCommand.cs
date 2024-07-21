using MediatR;

namespace UMS.Application.Classes.Commands.CreateSession
{
    public record CreateSessionCommand(
        TimeOnly StartTime,
        TimeOnly EndTime,
        long ClassId) : IRequest<long>;
}
