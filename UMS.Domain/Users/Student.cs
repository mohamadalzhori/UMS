using UMS.Domain.Classes;
using UMS.Domain.Users;

namespace UMS.Domain.Users
{
    public class Student : User
    {
        private readonly List<ClassEnrollment> _classEnrollments = new();
        public virtual IReadOnlyCollection<ClassEnrollment> ClassEnrollments => _classEnrollments.AsReadOnly();

        public void AddClassEnrollment(ClassEnrollment classEnrollment)
        {
            if (classEnrollment == null)
                throw new ArgumentNullException(nameof(classEnrollment));
        
            if (classEnrollment.StudentId != Id)
                throw new InvalidOperationException("StudentId must be the same as the student's Id");

            _classEnrollments.Add(classEnrollment);
        }

        
    }
}
