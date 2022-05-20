using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Repositories.Extensions;

public static class RepositoryClaimExtensions
{
    public static IQueryable<Claims> FilterClaims(this IQueryable<Claims> claims, int minId, int maxId) =>
          claims.Where(e => (e.Id >= minId && e.Id <= maxId));

    public static IQueryable<Claims> Search(this IQueryable<Claims> claims, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return claims;
        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return claims.Where(e => e.ClaimValue.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Claims> Sort(this IQueryable<Claims> claims, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return claims.OrderBy(e => e.ClaimValue);

        var orderQuery = Utility.OrderQueryBuilder.CreateOrderQuery<Claims>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return claims.OrderBy(e => e.ClaimValue);
        return claims;
        //return claims.OrderBy(orderQuery.ToString());
    }
}