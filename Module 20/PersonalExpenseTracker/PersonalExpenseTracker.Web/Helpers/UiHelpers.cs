using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Helpers;

public static partial class UiHelpers
{
    // Uses the request culture (en-GB) for thousand separators, e.g. "৳ 1,250.00".
    public static string Money(decimal amount) => $"৳ {amount:N2}";

    public static string ShortDate(DateTime date) => date.ToString("dd MMM yyyy");

    public static IHtmlContent CategoryBadge(this IHtmlHelper html, string name, string color, string icon)
    {
        var badge = new TagBuilder("span");
        badge.AddCssClass("cat-badge");
        badge.Attributes["style"] = $"--cat:{SafeColor(color)}";
        badge.InnerHtml.AppendHtml(Icon(icon));
        badge.InnerHtml.Append(name);
        return badge;
    }

    public static IHtmlContent CategoryAvatar(this IHtmlHelper html, string color, string icon, string? cssClass = null)
    {
        var avatar = new TagBuilder("span");
        avatar.AddCssClass("cat-avatar");
        if (cssClass is not null) avatar.AddCssClass(cssClass);
        avatar.Attributes["style"] = $"--cat:{SafeColor(color)}";
        avatar.InnerHtml.AppendHtml(Icon(icon));
        return avatar;
    }

    private static TagBuilder Icon(string icon)
    {
        var i = new TagBuilder("i");
        i.AddCssClass($"bi bi-{(CategoryAppearance.Icons.Contains(icon) ? icon : CategoryAppearance.DefaultIcon)}");
        i.Attributes["aria-hidden"] = "true";
        return i;
    }

    // Colours end up inside a style attribute, so only plain hex values are allowed through.
    private static string SafeColor(string color) =>
        HexColor().IsMatch(color) ? color : CategoryAppearance.DefaultColor;

    [GeneratedRegex("^#[0-9A-Fa-f]{6}$")]
    private static partial Regex HexColor();
}
