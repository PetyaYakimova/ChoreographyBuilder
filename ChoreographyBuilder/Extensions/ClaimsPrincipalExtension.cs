using static ChoreographyBuilder.Infrastructure.Constants.RoleConstants;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }

        public static bool IsUser(this ClaimsPrincipal user)
        {
            return user.IsInRole(UserRoleName);
        }
    }
}
