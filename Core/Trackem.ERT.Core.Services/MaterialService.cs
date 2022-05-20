using AutoMapper;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.Contracts;

namespace Trackem.ERT.Core.Services
{
    internal sealed class MaterialService: IMaterialService
    {
        IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IMaterialLink _link;
        public MaterialService(IRepositoryManager repository, IMapper mapper, IMaterialLink link)
        {
            _repository = repository;
            _mapper = mapper;
            _link = link;
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> FetchMaterialsAsync(int id, RequestParameters<MaterialParameter> linkParameters, bool trackChanges)
        {
            try
            {
                var data = await _repository.Material.GetMaterialsAsync(trackChanges, linkParameters.value);
                var model = _mapper.Map<IEnumerable<MaterialGridDetailResponse>>(data);
                var links = _link.TryGenerateLinks(model, linkParameters.value.Fields, 421, linkParameters.Context, false);
                return (linkResponse: links, metaData: data.MetaData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public async Task<(LinkResponse linkResponse, MetaData metaData)> FetchMaterialsAsync(int id, MatLinkParameters linkParameters, bool trackChanges)
        //{
        //    try
        //    {
        //        var data = await _repository.Material.GetMaterialsAsync(trackChanges, linkParameters.MaterialParameter);
        //        var model = _mapper.Map<IEnumerable<MaterialGridDetailResponse>>(data);
        //        var links = _link.TryGenerateLinks(model, linkParameters.MaterialParameter.Fields, 421, linkParameters.Context, false);
        //        return (linkResponse: links, metaData: data.MetaData);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public Task<IEnumerable<MaterialGridDetailResponse>> FetchMaterialByIdAsync(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
