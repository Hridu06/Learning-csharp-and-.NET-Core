using LibraryManagementSystem.Exceptions;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            // =========================
            // ADD ITEMS
            // =========================

            library.AddItem(new Book(1, "Clean Code", 2008, "Robert Martin"));

            library.AddItem(new Book(2, "C# in Depth", 2020, "Jon Skeet"));

            library.AddItem(new Magazine(3, "Science Monthly", 2024, 12, "February"));

            library.AddItem(new Magazine(4, "Tech World", 2023, 5, "June"));

            library.AddItem(new Audiobook(5, "Atomic Habits", 2019, "James Clear", 320));

            library.AddItem(new Audiobook(6, "Deep Work", 2018, "Cal Newport", 280));

            // =========================
            // REGISTER MEMBERS
            // =========================

            library.RegisterMember(new RegularMember(101, "Sabbir"));

            library.RegisterMember(new RegularMember(102, "Hridoy"));

            library.RegisterMember(new PremiumMember(103, "Karim"));

            Console.WriteLine("===== MEMBERS =====");

            foreach (var member in library.Members)
            {
                Console.WriteLine(member);
            }

            Console.WriteLine();

            // =========================
            // BORROW OPERATIONS
            // =========================

            try
            {
                library.BorrowItem(101, 1);

                library.BorrowItem(101, 2);

                library.BorrowItem(103, 3);

                library.BorrowItem(103, 5);

                // This should fail
                library.BorrowItem(102, 1);
            }
            catch (CannotBorrowException ex)
            {
                Console.WriteLine($"Cannot Borrow: {ex.Message}");
            }
            catch (MaximumItemsReachedException ex)
            {
                Console.WriteLine($"Limit Reached: {ex.Message}");
            }

            Console.WriteLine();

            // =========================
            // RENEW LOAN
            // =========================

            try
            {
                library.RenewLoan(1);

                // Second renew should fail
                library.RenewLoan(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();

            // =========================
            // RETURN ITEM
            // =========================

            try
            {
                library.ReturnItem(2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();

            // =========================
            // SEARCH BY TITLE
            // =========================

            Console.WriteLine("===== SEARCH RESULTS =====");

            var searchResults = library.FindItemsByTitle("Code");

            foreach (var item in searchResults)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            // =========================
            // OVERDUE DEMO
            // =========================

            // Making one overdue manually
            var overdueLoan = library.Loans.First();

            overdueLoan.DueDate = DateTime.Now.AddDays(-5);

            Console.WriteLine("===== OVERDUE LOANS =====");

            var overdueLoans = library.GetOverdueLoans();

            foreach (var overdue in overdueLoans)
            {
                Console.WriteLine(
                    $"Member: {overdue.MemberName} | " +
                    $"Item: {overdue.ItemTitle} | " +
                    $"Overdue Days: {overdue.OverdueDays} | " +
                    $"Fine: ${overdue.Fine}");
            }

            Console.WriteLine();

            // =========================
            // ALL ITEMS
            // =========================

            Console.WriteLine("===== ALL ITEMS =====");

            foreach (var item in library.Items)
            {
                Console.WriteLine(item);
            }
        }
    }
}