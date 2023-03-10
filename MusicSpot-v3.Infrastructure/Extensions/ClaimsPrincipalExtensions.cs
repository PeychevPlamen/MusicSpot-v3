namespace MusicSpot_v3.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static MusicSpot_v3.Infrastructure.Data.AdminConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
          => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
