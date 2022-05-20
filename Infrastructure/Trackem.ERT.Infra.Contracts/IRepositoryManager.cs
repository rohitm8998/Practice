namespace Trackem.ERT.Infra.Contracts;

public interface IRepositoryManager
{
    IClaimRepository Claim { get; }

    IClaimUIAccessRepository ClaimUIAccess { get; }

    IMaterialRepository Material { get; }

    Task SaveAsync();
}