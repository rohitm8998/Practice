using AutoMapper;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Contracts;

namespace Trackem.ERT.Core.Services;
public class ServiceManager : IServiceManager
{
    private readonly Lazy<IClaimService> _claimService;
    private readonly Lazy<IMaterialService> _materialService;
    //private readonly IMapper _mapper;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IClaimLink claimLink, IMaterialLink materialLink)
    {
        _claimService = new Lazy<IClaimService>(() => new ClaimService(repositoryManager, mapper, claimLink));
        _materialService = new Lazy<IMaterialService>(() => new MaterialService(repositoryManager, mapper, materialLink));
    }
    public IClaimService ClaimService => _claimService.Value;
    public IMaterialService MaterialService => _materialService.Value;
}

