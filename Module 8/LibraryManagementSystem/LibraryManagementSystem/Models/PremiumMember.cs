namespace LibraryManagementSystem.Models
{
    public class PremiumMember : LibraryMember
    {
        public override int MaxBorrowLimit => 12;

        public override int ExtraLoanDays => 7;

        public PremiumMember(int memberId, string name)
            : base(memberId, name)
        {
        }

        public override string ToString()
        {
            return $"[Premium Member] {base.ToString()}";
        }
    }
}