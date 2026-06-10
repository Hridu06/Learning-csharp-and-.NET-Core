namespace LibraryManagementSystem.Interfaces
{
    public interface IBorrowable
    {
        bool IsAvailable();

        void BorrowItem();

        void ReturnItem();
    }
}