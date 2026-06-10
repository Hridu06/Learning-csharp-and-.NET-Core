namespace LibraryManagementSystem.Models
{
    public record OverdueLoanInfo(
        string MemberName,
        string ItemTitle,
        DateTime DueDate,
        int OverdueDays,
        double Fine
    );
}