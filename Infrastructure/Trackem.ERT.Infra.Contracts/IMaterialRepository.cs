using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Contracts;
public interface IMaterialRepository
{
    Task<PagedList<Material>> GetMaterialsAsync(bool trackChanges, MaterialParameter materialParameter);
    Task<Material> GetMaterialAsync(long id, bool trackChanges);
}