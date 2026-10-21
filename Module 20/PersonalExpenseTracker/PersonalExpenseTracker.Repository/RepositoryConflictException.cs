namespace PersonalExpenseTracker.Repository;

public enum ConflictKind { Duplicate, RelatedRecord }

public sealed class RepositoryConflictException(ConflictKind kind, Exception innerException)
    : Exception("The database rejected a conflicting change.", innerException)
{
    public ConflictKind Kind { get; } = kind;
}
