using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Contracts;
public interface IClaimRepository
{
    //Task<IQueryable<Claims>> GetAllClaimsAsync(bool trackChanges);
    Task<PagedList<Claims>> GetClaimsAsync(bool trackChanges, ClaimParameter claimParameter);
    Task<Claims> GetClaimAsync(int id, bool trackChanges);
}
