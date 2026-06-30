using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string ISBN { get; set; } = string.Empty;

        public int PublicationYear { get; set; }

        [Range(0, int.MaxValue)]
        public int TotalCopies { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }

        // Navigation Property
        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }

        // Navigation Property
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}