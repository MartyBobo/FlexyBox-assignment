namespace FlexyBox.Services;

public class CurrentUserService : ICurrentUserService
{
    // Hard-coded default user ID - in a real app this would come from authentication
    public int UserId { get; } = 1;
}
