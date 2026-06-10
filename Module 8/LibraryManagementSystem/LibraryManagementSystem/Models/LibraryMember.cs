namespace LibraryManagementSystem.Models
{
    public abstract class LibraryMember
    {
        public int MemberId { get; private set; }

        public string Name { get; set; }

        public List<Loan> Loans { get; private set; }

        public abstract int MaxBorrowLimit { get; }

        public virtual int ExtraLoanDays => 0;

        protected LibraryMember(int memberId, string name)
        {
            MemberId = memberId;
            Name = name;
            Loans = new List<Loan>();
        }

        public int CurrentBorrowedItems =>
            Loans.Count(l => l.ReturnDate == null);

        public override string ToString()
        {
            return $"{MemberId} - {Name} | Borrowed: {CurrentBorrowedItems}/{MaxBorrowLimit}";
        }
    }
}