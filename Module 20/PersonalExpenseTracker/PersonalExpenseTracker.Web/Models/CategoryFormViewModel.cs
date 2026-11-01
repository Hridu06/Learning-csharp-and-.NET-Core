using System.ComponentModel.DataAnnotations;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Models;

public class CategoryFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a category name.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please choose a colour.")]
    [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage = "Please choose a valid colour.")]
    [Display(Name = "Colour")]
    public string Color { get; set; } = CategoryAppearance.DefaultColor;

    [Required(ErrorMessage = "Please choose an icon.")]
    public string Icon { get; set; } = CategoryAppearance.DefaultIcon;
}
