namespace UMS.API.Controllers.Courses
{
    public record CreateCourseRequest(
    string Name,
    int MaxStudentNumber,
    DateOnly StartDate,
    DateOnly EndDate
    );
}
