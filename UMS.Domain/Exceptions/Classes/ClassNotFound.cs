namespace UMS.Domain.Exceptions.Classes;

public class ClassNotFound(long classId) : Exception($"Class with id {classId} not found");