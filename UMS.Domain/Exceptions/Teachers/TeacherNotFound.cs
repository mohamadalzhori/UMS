namespace UMS.Domain.Exceptions.Teachers;

public class TeacherNotFound(long teacherId) : Exception($"Teacher with ID {teacherId} not found");