using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Models
{
    public enum ItemStatus
    {
        Available,
        Borrowed,
        Reserved,
        Damaged
    }

    public abstract class LibraryItem : IBorrowable
    {
        private int _publicationYear;

        public int Id { get; private set; }

        public string Title { get; set; }

        public int PublicationYear
        {
            get => _publicationYear;
            set
            {
                if (value > DateTime.Now.Year)
                    throw new ArgumentException("Publication year cannot be in the future.");

                _publicationYear = value;
            }
        }

        public ItemStatus Status { get; set; }

        public virtual int MaxBorrowDays { get; protected set; }

        protected LibraryItem(int id, string title, int publicationYear)
        {
            Id = id;
            Title = title;
            PublicationYear = publicationYear;
            Status = ItemStatus.Available;
        }

        public override string ToString()
        {
            return $"{Id} - {Title} ({PublicationYear}) | Status: {Status}";
        }

        public bool IsAvailable()
        {
            return Status == ItemStatus.Available;
        }

        public void BorrowItem()
        {
            Status = ItemStatus.Borrowed;
        }

        public void ReturnItem()
        {
            Status = ItemStatus.Available;
        }
    }
}