namespace PersonalExpenseTracker.Service.Interfaces;

public interface ICurrentUser
{
    // Throws when nobody is signed in; every data page requires authentication.
    string UserId { get; }
}
