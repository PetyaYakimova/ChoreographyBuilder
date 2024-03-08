using System.Security.Claims;

namespace ChoreographyBuilder.Extensions
{
	public static class ClaimsPrincipalExtension
	{
		public static string Id(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
