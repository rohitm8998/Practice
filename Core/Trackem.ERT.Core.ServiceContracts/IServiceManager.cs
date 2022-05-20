namespace Trackem.ERT.Core.ServiceContracts;

public interface IServiceManager
{
    IClaimService ClaimService { get; }
    IMaterialService MaterialService { get; }
}

