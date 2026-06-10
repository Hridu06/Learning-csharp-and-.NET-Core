namespace LibraryManagementSystem.Exceptions
{
    public class MaximumItemsReachedException : Exception
    {
        public MaximumItemsReachedException(string message)
            : base(message)
        {
        }
    }
}