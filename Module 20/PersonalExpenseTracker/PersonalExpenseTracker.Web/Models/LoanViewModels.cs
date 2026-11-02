using System.ComponentModel.DataAnnotations;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Models;

public class LoanFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Choose whether you borrowed or lent the money.")]
    public LoanType? Type { get; set; }

    [Required(ErrorMessage = "Please enter a name.")]
    [StringLength(100)]
    [Display(Name = "Person or lender")]
    public string Person { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter an amount.")]
    [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "The amount must be greater than zero.")]
    [Display(Name = "Amount (BDT)")]
    public decimal? Principal { get; set; }

    [Required(ErrorMessage = "Please choose a date.")]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Due date")]
    public DateTime? DueDate { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }
}

public class LoanPaymentFormModel
{
    [Required(ErrorMessage = "Please enter an amount.")]
    [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "The amount must be greater than zero.")]
    [Display(Name = "Amount (BDT)")]
    public decimal? Amount { get; set; }

    [Required(ErrorMessage = "Please choose a date.")]
    [DataType(DataType.Date)]
    public DateTime? Date { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}

// Tab is "active", "borrowed", "lent" or "settled".
public record LoansIndexViewModel(IReadOnlyList<LoanDto> Loans, LoanSummaryDto Summary, string Tab, int SettledCount);

public record LoanDetailsViewModel(LoanDetailDto Detail, LoanPaymentFormModel Payment);
