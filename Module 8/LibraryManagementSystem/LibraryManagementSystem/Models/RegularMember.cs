namespace LibraryManagementSystem.Models
{
    public class RegularMember : LibraryMember
    {
        public override int MaxBorrowLimit => 5;

        public RegularMember(int memberId, string name)
            : base(memberId, name)
        {
        }

        public override string ToString()
        {
            return $"[Regular Member] {base.ToString()}";
        }
    }
}