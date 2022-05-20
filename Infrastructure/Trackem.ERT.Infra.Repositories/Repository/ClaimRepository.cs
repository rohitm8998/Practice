using Microsoft.EntityFrameworkCore;
using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.Contracts;
using Trackem.ERT.Infra.EFCore;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Repositories.Repository;
public class ClaimRepository : RepositoryBase<Claims>, IClaimRepository
{
    public ClaimRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<Claims>> GetClaimsAsync(bool trackChanges, ClaimParameter claimParameter)
    {
        try
        {
            var data = FindAll(trackChanges)
                /* Below belongs RepositoryClaimExtensions
                .FilterClaims(claimParameter.MinId, claimParameter.MaxId)
                .Search(claimParameter.SearchTerm)
                .Sort(claimParameter.OrderBy)
                */
                .Skip((claimParameter.PageNumber - 1) * claimParameter.PageSize)
               .Take(claimParameter.PageSize)
               .AsQueryable();

            var result = data.ToList();

            var count = await FindAll(trackChanges)
                //    .FilterClaims(claimParameter.MinId, claimParameter.MaxId)
                //    .Search(claimParameter.SearchTerm)
                .CountAsync();

            PagedList<Claims> claims = new PagedList<Claims>(result, count, claimParameter.PageNumber, claimParameter.PageSize);
            return claims; // Performance Better but can switch to above one too
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Claims> GetClaimAsync(int id, bool trackChanges) => await FindByCondition(e => e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
}

