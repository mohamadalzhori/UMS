using UMS.Domain.Classes;
using UMS.Domain.Shared;

namespace UMS.Domain.Courses;

public partial class Course
{
    private Course()
    {
        // required by EF
    }

    private Course(long id, Name name, MaxStudentNumber maxStudentNumber, DateOnly startDate, DateOnly endDate)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        MaxStudentsNumber = maxStudentNumber ?? throw new ArgumentNullException(nameof(maxStudentNumber));
        SetEnrollmentPeriod(startDate, endDate);
    }

    public long Id { get; init; }
    public Name Name { get; set; } = null!; // I'm sure this is not gonna be null
    public MaxStudentNumber MaxStudentsNumber { get; set; }

    public DateOnly? EnrollmentStartDate { get; private set; }
    public DateOnly? EnrollmentEndDate { get; private set; }

    public void SetEnrollmentPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Enrollment end date cannot precede start date.");

        EnrollmentStartDate = startDate;
        EnrollmentEndDate = endDate;
    }

    private readonly List<Class> classes = new();
    public virtual IReadOnlyCollection<Class> Classes => classes.AsReadOnly();


    // using @ since class is a reserved keyword
    public void AddClass(Class @class)
    {
        if (@class == null)
            throw new ArgumentNullException(nameof(@class));

        classes.Add(@class);
    }

    public void RemoveClass(Class @class)
    {
        if (@class == null)
            throw new ArgumentNullException(nameof(@class));

        classes.Remove(@class);
    }

    public static Course Create(Name name, MaxStudentNumber maxStudentNumber, DateOnly startDate, DateOnly endDate)
    {
        // we can add more validation here, or triggering events

        // zero id means it's a new course, db will assign a proper id when saved

        return new Course(0, name, maxStudentNumber, startDate, endDate);
    }

}
