using UMS.Domain.Classes;
using UMS.Domain.Users;

namespace UMS.Domain.Users
{
    public class Student : User
    {
        private readonly List<ClassEnrollment> _classEnrollments = new();
        public virtual IReadOnlyCollection<ClassEnrollment> ClassEnrollments => _classEnrollments.AsReadOnly();

        public string? PicturePath { get; set; }
    }
}
