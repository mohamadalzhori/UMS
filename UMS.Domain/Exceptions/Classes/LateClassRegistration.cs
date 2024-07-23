namespace UMS.Domain.Exceptions.Classes;

public class LateClassRegistration(DateOnly? endDate) : Exception($"Class registration ended on {endDate}");