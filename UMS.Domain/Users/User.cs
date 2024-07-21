using UMS.Domain.Shared;

namespace UMS.Domain.Users;

public abstract class User
{
    public long Id { get; init; }
    public Name Name { get; set; } = null!;
    public Email Email { get; set; } = null!;

}
