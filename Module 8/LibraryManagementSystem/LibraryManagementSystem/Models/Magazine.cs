namespace LibraryManagementSystem.Models
{
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }

        public string PublicationMonth { get; set; }

        public override int MaxBorrowDays => 7;

        public Magazine(int id, string title, int publicationYear,
            int issueNumber, string publicationMonth)
            : base(id, title, publicationYear)
        {
            IssueNumber = issueNumber;
            PublicationMonth = publicationMonth;
        }

        public override string ToString()
        {
            return $"[Magazine] {base.ToString()} | Issue: {IssueNumber} | Month: {PublicationMonth}";
        }
    }
}