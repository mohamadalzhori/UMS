namespace UMS.Application.Students.Queries.GetAllStudents;

public class StudentDto
{
    public long Id { get; init; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}