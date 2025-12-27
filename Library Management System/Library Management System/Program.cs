class LibraryItem
{
    public string Title { get; set; }
    public int ItemId { get; set; }
    public bool IsAvailable { get; set; }

    public LibraryItem(string title, int itemId, bool isAvailable = true)
    {
        Title = title;
        ItemId = itemId;
        IsAvailable = isAvailable;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Item ID: {ItemId}");
        Console.WriteLine($"Available: {(IsAvailable ? "Yes" : "No")}");
    }
}

class Book : LibraryItem
{
    public string Author { get; set; }

    public Book(string title, int itemId, string author, bool isAvailable = true)
        : base(title, itemId, isAvailable)
    {
        Author = author;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Author: {Author}");
    }
}

class Magazine : LibraryItem
{
    public int IssueNumber { get; set; }

    public Magazine(string title, int itemId, int issueNumber, bool isAvailable = true)
        : base(title, itemId, isAvailable)
    {
        IssueNumber = issueNumber;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Issue Number: {IssueNumber}");
    }
}

class Program
{
    public static void Main(string[] args)
    {
       
        var book1 = new Book("1984", 1, "George Orwell");
        var book2 = new Book("Clean Code", 2, "Robert C. Martin");
        var magazine = new Magazine("National Geographic", 1001, 202);

        
        Console.WriteLine("Library items:");
        book1.DisplayInfo();
        Console.WriteLine();
        book2.DisplayInfo();
        Console.WriteLine();
        magazine.DisplayInfo();
        Console.WriteLine();

        
        Console.WriteLine("Borrowing item: 1984 (Item ID 1)");
        book1.IsAvailable = false;
        Console.WriteLine();

        
        Console.WriteLine("Updated item info for borrowed book:");
        book1.DisplayInfo();
    }
}
