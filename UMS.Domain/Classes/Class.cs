using System;
using UMS.Domain.Courses;
using UMS.Domain.Exceptions;
using UMS.Domain.Exceptions.Classes;
using UMS.Domain.Users;
namespace UMS.Domain.Classes;

// Class represents a course that a teacher teaches and students can enroll in.
public partial class Class
{
    public Class(long id, long teacherId, long courseId)
    {
        Id = id;
        TeacherId = teacherId;
        CourseId = courseId;
    }

    // we need id, teacherId and courseId can't be unique because a teacher can teach the same course at different times
    public long Id { get; init; }

    public long TeacherId { get; private set; }
    public virtual Teacher Teacher { get; private set; } = null!;


    public long CourseId { get; private set; }
    public virtual Course Course { get; private set; } = null!;

    // students are enrolled in a class not a course, because a class doesn't have a specific timing
    private readonly List<ClassEnrollment> _classEnrollments = new();
    public virtual IReadOnlyCollection<ClassEnrollment> ClassEnrollments => _classEnrollments.AsReadOnly();


    private readonly List<Session> _sessions = new();
    public virtual IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    public static Class Register(long teacherId, long courseId)
    {
        var @class = new Class(0, teacherId, courseId);

        return @class;
    }

    public Session AddSession(TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime < startTime)
            throw new InvalidSessionTime();

        var overlap = Sessions.Any(s => (startTime >= s.StartTime && startTime <= s.EndTime) || (endTime >= s.StartTime && endTime <= s.EndTime));

        if (overlap)
            throw new SessionOverlap();

        var session = Session.Create(startTime, endTime, Id);

        _sessions.Add(session);

        return session;
    }

    public void Enroll(Student student)
    {
        if (Course.MaxStudentsNumber is not null && ClassEnrollments.Count >= Course.MaxStudentsNumber.Value)
            throw new FullClass();

        if (DateOnly.FromDateTime(DateTime.Now) < Course.EnrollmentStartDate)
            throw new EarlyClassRegistration(Course.EnrollmentStartDate);

        if (DateOnly.FromDateTime(DateTime.Now) > Course.EnrollmentEndDate)
            throw new LateClassRegistration(Course.EnrollmentEndDate);

        var enrollment = ClassEnrollment.Create(this, student); 

        _classEnrollments.Add(enrollment);
    }
    
}
