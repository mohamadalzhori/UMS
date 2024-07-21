namespace UMS.Domain.Courses
{
    public record class MaxStudentNumber
    {
        public int Value { get; private set; }

        public MaxStudentNumber() { }
        public MaxStudentNumber(int maxStudents)
        {
            if (maxStudents < 0)
                throw new ArgumentException("Maximum number of students cannot be negative.");

            Value = maxStudents; 
        }
    }
}
