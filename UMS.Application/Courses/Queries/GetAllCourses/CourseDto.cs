namespace UMS.API.Controllers.Courses
{
    public record CourseDto
    {
        public long Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int MaxStudentNumber { get; init; }
        public DateOnly? EnrollmentStartDate { get; init; }
        public DateOnly? EnrollmentEndDate { get; init; }
    }
}
