using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }

        // Foreign Key
        public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string StudentName { get; set; } = string.Empty;

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        // Computed Property
        [NotMapped]
        public bool IsReturned => ReturnDate.HasValue;

        // Navigation Property
        [ForeignKey("BookId")]
        public Book? Book { get; set; }
    }
}