namespace PersonalExpenseTracker.Repository.Models;

public record CategoryAggregate(int Id, string Name, string? Description, string Color, string Icon,
    int ExpenseCount, decimal Total);
