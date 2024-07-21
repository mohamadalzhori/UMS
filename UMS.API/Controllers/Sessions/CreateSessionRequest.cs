namespace UMS.API.Controllers.Sessions
{
    public record CreateSessionRequest(
        TimeOnly StartTime,
        TimeOnly EndTime,
        long ClassId);
}