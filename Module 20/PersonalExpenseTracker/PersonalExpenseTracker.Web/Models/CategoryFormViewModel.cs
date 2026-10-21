using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Models;

public class CategoryFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a category name.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
}
