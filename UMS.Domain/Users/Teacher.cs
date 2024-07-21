using UMS.Domain.Classes;

namespace UMS.Domain.Users
{
    public class Teacher : User
    {
        private readonly List<Class> _classes = new();
        public virtual IReadOnlyCollection<Class> Classes => _classes.AsReadOnly();

        public void AddClass(Class @class)
        {
            if (@class == null)
                throw new ArgumentNullException(nameof(@class));

            if (@class.TeacherId != Id)
                throw new InvalidOperationException("TeacherId must be the same as the teacher's Id");

            _classes.Add(@class);
        }

    }
}
