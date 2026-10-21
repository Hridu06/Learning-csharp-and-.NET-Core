using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalExpenseTracker.Web.Models;

public class ExpenseFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter an expense title.")]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter an amount.")]
    [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "The amount must be greater than zero.")]
    [Display(Name = "Amount (BDT)")]
    public decimal? Amount { get; set; }

    [Required(ErrorMessage = "Please choose a date.")]
    [DataType(DataType.Date)]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "Please select a category.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }

    [Display(Name = "Receipt image")]
    public IFormFile? Receipt { get; set; }

    [BindNever]
    public string? ExistingReceiptFileName { get; set; }

    [BindNever]
    public IReadOnlyList<SelectListItem> Categories { get; set; } = [];
}
