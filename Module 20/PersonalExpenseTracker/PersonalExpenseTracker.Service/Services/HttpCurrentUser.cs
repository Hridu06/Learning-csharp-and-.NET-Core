using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PersonalExpenseTracker.Service.Interfaces;

namespace PersonalExpenseTracker.Service.Services;

public class HttpCurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public string UserId => accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("This operation requires a signed-in user.");
}
