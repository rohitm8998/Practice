using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;

namespace Trackem.ERT.Core.ServiceContracts;

public interface IClaimService
{
    //Task<(LinkResponse linkResponse, MetaData metaData)> FetchClaimsAsync(int id, ClaimRequestParameters linkParameters, bool trackChanges);
    Task<(LinkResponse linkResponse, MetaData metaData)> FetchClaimsAsync(int id, RequestParameters<ClaimParameter> linkParameters, bool trackChanges);
    Task<IEnumerable<ClaimViewModel>> FetchClaimByIdAsync(int id, bool trackChanges);
}

