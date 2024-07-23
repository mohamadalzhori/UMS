namespace UMS.Domain.Exceptions.Classes;

public class InvalidSessionTime() : Exception("End Time must not precede Start Time");