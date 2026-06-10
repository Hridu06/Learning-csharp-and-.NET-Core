namespace LibraryManagementSystem.Models
{
    public class Book : LibraryItem
    {
        public string Author { get; set; }

        public override int MaxBorrowDays => 14;

        public Book(int id, string title, int publicationYear, string author)
            : base(id, title, publicationYear)
        {
            Author = author;
        }

        public override string ToString()
        {
            return $"[Book] {base.ToString()} | Author: {Author}";
        }
    }
}