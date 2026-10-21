namespace PersonalExpenseTracker.Repository.Models;

public record CategoryAggregate(int Id, string Name, string? Description, int ExpenseCount, decimal Total);
