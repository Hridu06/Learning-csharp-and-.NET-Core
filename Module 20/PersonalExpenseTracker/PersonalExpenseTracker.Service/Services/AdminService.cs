using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class AdminService(UserManager<ApplicationUser> users, IUserRepository userData,
    ICurrentUser currentUser, TimeProvider clock) : IAdminService
{
    public async Task<AdminDashboardDto> GetDashboardAsync(CancellationToken ct = default)
    {
        var now = clock.GetUtcNow();
        var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var all = await users.Users.AsNoTracking().OrderByDescending(u => u.CreatedAt).ToListAsync(ct);
        var adminIds = (await users.GetUsersInRoleAsync(AppRoles.Admin)).Select(u => u.Id).ToHashSet();
        var counts = await userData.GetExpenseCountsAsync(ct);
        var me = currentUser.UserId;

        var rows = all.Select(u => new AdminUserDto(u.Id, u.FullName, u.Email, u.CreatedAt,
            counts.GetValueOrDefault(u.Id), adminIds.Contains(u.Id), u.LockoutEnd > now, u.Id == me)).ToList();
        return new AdminDashboardDto(rows, all.Count(u => u.CreatedAt >= monthStart), await userData.CountExpensesAsync(ct));
    }

    public async Task<OperationResult> SetLockedAsync(string userId, bool locked)
    {
        if (userId == currentUser.UserId) return OperationResult.Fail("You can't lock your own account.");
        var user = await users.FindByIdAsync(userId);
        if (user is null) return OperationResult.Missing();

        if (locked)
        {
            await users.SetLockoutEnabledAsync(user, true);
            await users.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        }
        else
        {
            await users.SetLockoutEndDateAsync(user, null);
            await users.ResetAccessFailedCountAsync(user);
        }
        // Existing sessions are rejected at the next security stamp check (see ValidationInterval).
        await users.UpdateSecurityStampAsync(user);
        return OperationResult.Success();
    }

    public async Task<OperationResult> SetAdminAsync(string userId, bool isAdmin)
    {
        // Admins can't demote themselves, which also guarantees at least one admin always remains.
        if (!isAdmin && userId == currentUser.UserId) return OperationResult.Fail("You can't remove your own admin role.");
        var user = await users.FindByIdAsync(userId);
        if (user is null) return OperationResult.Missing();

        var result = isAdmin ? await users.AddToRoleAsync(user, AppRoles.Admin) : await users.RemoveFromRoleAsync(user, AppRoles.Admin);
        if (!result.Succeeded) return OperationResult.Fail(string.Join(" ", result.Errors.Select(e => e.Description)));
        // Refreshes the user's role claims at their next security stamp check.
        await users.UpdateSecurityStampAsync(user);
        return OperationResult.Success();
    }
}
