using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Helpers;

public static partial class UiHelpers
{
    // Uses the request culture (en-GB) for thousand separators, e.g. "৳ 1,250.00".
    public static string Money(decimal amount) => $"৳ {amount:N2}";

    public static string ShortDate(DateTime date) => date.ToString("dd MMM yyyy");

    // "Sabbir Hossain Hridoy" -> "SH"
    public static string Initials(string name)
    {
        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length switch
        {
            0 => "?",
            1 => parts[0][..1].ToUpperInvariant(),
            _ => $"{parts[0][0]}{parts[1][0]}".ToUpperInvariant()
        };
    }

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
        var known = CategoryAppearance.Icons.Contains(icon) || IncomeSources.All.Any(s => s.Icon == icon);
        i.AddCssClass($"bi bi-{(known ? icon : CategoryAppearance.DefaultIcon)}");
        i.Attributes["aria-hidden"] = "true";
        return i;
    }

    // Income sources reuse the category badge/avatar look with their fixed colour and icon.
    public static IHtmlContent SourceBadge(this IHtmlHelper html, IncomeSource source)
    {
        var info = IncomeSources.Get(source);
        return html.CategoryBadge(info.Label, info.Color, info.Icon);
    }

    public static IHtmlContent SourceAvatar(this IHtmlHelper html, IncomeSource source, string? cssClass = null)
    {
        var info = IncomeSources.Get(source);
        return html.CategoryAvatar(info.Color, info.Icon, cssClass);
    }

    // Colours end up inside a style attribute, so only plain hex values are allowed through.
    private static string SafeColor(string color) =>
        HexColor().IsMatch(color) ? color : CategoryAppearance.DefaultColor;

    [GeneratedRegex("^#[0-9A-Fa-f]{6}$")]
    private static partial Regex HexColor();
}
