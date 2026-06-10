namespace LibraryManagementSystem.Models
{
    public class Loan
    {
        public LibraryItem Item { get; private set; }

        public LibraryMember Member { get; private set; }

        public DateTime BorrowDate { get; private set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; private set; }

        public bool IsRenewed { get; set; }

        public Loan(LibraryItem item, LibraryMember member)
        {
            Item = item;
            Member = member;

            BorrowDate = DateTime.Now;

            DueDate = BorrowDate.AddDays(
                item.MaxBorrowDays + member.ExtraLoanDays);
        }

        public void ReturnItem()
        {
            ReturnDate = DateTime.Now;
        }

        public double CalculateFine()
        {
            DateTime compareDate = ReturnDate ?? DateTime.Now;

            if (compareDate <= DueDate)
                return 0;

            int overdueDays = (compareDate - DueDate).Days;

            return overdueDays * 1.5;
        }

        public override string ToString()
        {
            return $"{Item.Title} borrowed by {Member.Name} | Due: {DueDate:d}";
        }
    }
}