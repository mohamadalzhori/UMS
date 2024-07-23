namespace UMS.Domain.Exceptions.Students;

public class StudentNotFound(long studentId) : Exception($"Student with id {studentId} not found");