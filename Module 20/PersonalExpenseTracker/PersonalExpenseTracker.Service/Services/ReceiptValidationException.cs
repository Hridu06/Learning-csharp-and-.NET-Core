namespace PersonalExpenseTracker.Service.Services;

public sealed class ReceiptValidationException(string message) : Exception(message);
