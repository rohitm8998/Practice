using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;

namespace Trackem.ERT.Core.ServiceContracts;

public interface IMaterialService
{
    //Task<(LinkResponse linkResponse, MetaData metaData)> FetchMaterialsAsync(int id, MatLinkParameters linkParameters, bool trackChanges);
    Task<(LinkResponse linkResponse, MetaData metaData)> FetchMaterialsAsync(int id, RequestParameters<MaterialParameter> linkParameters, bool trackChanges);
    Task<IEnumerable<MaterialGridDetailResponse>> FetchMaterialByIdAsync(int id, bool trackChanges);

}

