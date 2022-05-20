using Trackem.ERT.Infra.Contracts;
using Trackem.ERT.Infra.EFCore;
using Trackem.ERT.Infra.Repositories.Repository;

namespace Trackem.ERT.Infra.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private RepositoryContext _repositoryContext;
    private Lazy<IClaimRepository> _claimRepository;
    private Lazy<IClaimUIAccessRepository> _claimUIAccessRepository;
    private Lazy<IMaterialRepository> _materialRepository;
    //private IClaimRepository _claimRepository;
    //private IClaimUIAccessRepository _claimUIAccessRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _claimRepository = new Lazy<IClaimRepository>(() => new ClaimRepository(repositoryContext));
        _claimUIAccessRepository = new Lazy<IClaimUIAccessRepository>(() => new ClaimUIAccessRepository(repositoryContext));
        _materialRepository = new Lazy<IMaterialRepository>(() => new MaterialRepository(repositoryContext));
    }

    public IClaimRepository Claim => _claimRepository.Value;
    public IClaimUIAccessRepository ClaimUIAccess => _claimUIAccessRepository.Value;
    public IMaterialRepository Material => _materialRepository.Value;
    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();

    //public IClaimRepository Claim
    //{
    //    get
    //    {
    //        if (_claimRepository == null) _claimRepository = new ClaimRepository(_repositoryContext); return _claimRepository;
    //    }
    //}

    //public IClaimUIAccessRepository ClaimUIAccess
    //{
    //    get
    //    {
    //        if (_claimRepository == null) _claimRepository = new ClaimUIAccessRepository(_repositoryContext); return _claimUIAccessRepository;
    //    }
    //}


}