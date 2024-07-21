using UMS.Domain.Users;

namespace UMS.Domain.Classes;

public partial class ClassEnrollment
{
    // there is no need for a separate Id property

    // classId and studentId together are the primary key

    public ClassEnrollment(long classId, long studentId)
    {
        ClassId = classId;
        StudentId = studentId;
    }

    public long ClassId { get; private set; }
    public virtual Class Class { get; private set; } = null!;

    public long StudentId { get; private set; }
    public virtual Student Student { get; private set; } = null!;


    public static ClassEnrollment Create(Class @class, Student student)
    {
        return new ClassEnrollment(@class.Id, student.Id);
    }

}
