using LibraryManagementSystem.Exceptions;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class Library
    {
        public List<LibraryItem> Items { get; private set; }

        public List<LibraryMember> Members { get; private set; }

        public List<Loan> Loans { get; private set; }

        public Library()
        {
            Items = new List<LibraryItem>();
            Members = new List<LibraryMember>();
            Loans = new List<Loan>();
        }

        public void AddItem(LibraryItem item)
        {
            Items.Add(item);
        }

        public void RegisterMember(LibraryMember member)
        {
            Members.Add(member);
        }

        public void BorrowItem(int memberId, int itemId)
        {
            LibraryMember member = Members.FirstOrDefault(m => m.MemberId == memberId);

            LibraryItem item = Items.FirstOrDefault(i => i.Id == itemId);

            if (member == null)
                throw new Exception("Member not found.");

            if (item == null)
                throw new Exception("Item not found.");

            if (item.Status != ItemStatus.Available)
                throw new CannotBorrowException("Item is not available.");

            if (member.CurrentBorrowedItems >= member.MaxBorrowLimit)
                throw new MaximumItemsReachedException(
                    "Member has reached maximum borrowing limit.");

            Loan loan = new Loan(item, member);

            Loans.Add(loan);

            member.Loans.Add(loan);

            item.Status = ItemStatus.Borrowed;

            Console.WriteLine($"'{item.Title}' borrowed successfully by {member.Name}");
        }

        public void ReturnItem(int itemId)
        {
            Loan loan = Loans
                .FirstOrDefault(l =>
                    l.Item.Id == itemId &&
                    l.ReturnDate == null);

            if (loan == null)
                throw new Exception("Active loan not found.");

            loan.ReturnItem();

            loan.Item.Status = ItemStatus.Available;

            double fine = loan.CalculateFine();

            Console.WriteLine($"'{loan.Item.Title}' returned successfully.");

            if (fine > 0)
            {
                Console.WriteLine($"Late Fine: ${fine}");
            }
        }

        public void RenewLoan(int itemId)
        {
            Loan loan = Loans
                .FirstOrDefault(l =>
                    l.Item.Id == itemId &&
                    l.ReturnDate == null);

            if (loan == null)
                throw new Exception("Loan not found.");

            if (loan.IsRenewed)
                throw new Exception("Loan already renewed once.");

            loan.DueDate = loan.DueDate.AddDays(7);

            loan.IsRenewed = true;

            Console.WriteLine($"Loan renewed for '{loan.Item.Title}'");
        }

        public List<OverdueLoanInfo> GetOverdueLoans()
        {
            return Loans
                .Where(l =>
                    l.ReturnDate == null &&
                    l.DueDate < DateTime.Now)
                .Select(l =>
                    new OverdueLoanInfo(
                        l.Member.Name,
                        l.Item.Title,
                        l.DueDate,
                        (DateTime.Now - l.DueDate).Days,
                        l.CalculateFine()
                    ))
                .ToList();
        }

        public List<LibraryItem> FindItemsByTitle(string keyword)
        {
            return Items
                .Where(i =>
                    i.Title.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}