using AutoMapper;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.Contracts;

namespace Trackem.ERT.Core.Services;

internal sealed class ClaimService : IClaimService

{
    IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly IClaimLink _claimLink; 
    public ClaimService(IRepositoryManager repository, IMapper mapper, IClaimLink claimLink)
    {
        _repository = repository;
        _mapper = mapper;
        _claimLink = claimLink;
    }

    //public async Task<(LinkResponse linkResponse, MetaData metaData)> FetchClaimsAsync(int id, ClaimRequestParameters linkParameters, bool trackChanges)
    //{
    //    try
    //    {
    //        var claimWithMetaData = await _repository.Claim.GetClaimsAsync(trackChanges, linkParameters.ClaimParameter);
    //        var model = _mapper.Map<IEnumerable<ClaimViewModel>>(claimWithMetaData);
    //        var links = _claimLink.TryGenerateLinks(model, linkParameters.ClaimParameter.Fields, 421, linkParameters.Context, false);
    //        return (linkResponse: links, metaData: claimWithMetaData.MetaData);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    public async Task<(LinkResponse linkResponse, MetaData metaData)> FetchClaimsAsync(int id, RequestParameters<ClaimParameter> linkParameters, bool trackChanges)
    {
        try
        {
            var claimWithMetaData = await _repository.Claim.GetClaimsAsync(trackChanges, linkParameters.value);
            var model = _mapper.Map<IEnumerable<ClaimViewModel>>(claimWithMetaData);
            var links = _claimLink.TryGenerateLinks(model, linkParameters.value.Fields, 421, linkParameters.Context, false);
            return (linkResponse: links, metaData: claimWithMetaData.MetaData);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ClaimViewModel>> FetchClaimByIdAsync(int id, bool trackChanges)
    {
        try
        {
            var data = await _repository.Claim.GetClaimAsync(id, trackChanges);
            var model = _mapper.Map<IEnumerable<ClaimViewModel>>(data);
            return model;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
