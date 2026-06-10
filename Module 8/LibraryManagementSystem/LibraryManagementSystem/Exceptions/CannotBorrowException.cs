namespace LibraryManagementSystem.Exceptions
{
    public class CannotBorrowException : Exception
    {
        public CannotBorrowException(string message)
            : base(message)
        {
        }
    }
}