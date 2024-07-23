namespace UMS.Domain.Exceptions.Courses;

public class CourseNotFound(long courseId) : Exception($"Course with ID {courseId} not found");