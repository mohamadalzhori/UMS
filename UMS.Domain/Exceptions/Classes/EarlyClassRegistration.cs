namespace UMS.Domain.Exceptions.Classes;

public class EarlyClassRegistration(DateOnly? startDate) : Exception($"Class registration starts on {startDate}");