using Microsoft.AspNetCore.Http;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Infra.Common.Entities.LinkModels;

namespace Trackem.ERT.Core.ServiceContracts;
public interface IMaterialLink
{
    LinkResponse TryGenerateLinks(IEnumerable<MaterialGridDetailResponse> materialGridDetailResponses, string fields, int Id, HttpContext httpContext, bool hateoss);
    //LinkResponse TryGenerateLinks(IEnumerable<MaterialGridDetailResponse> materialGridDetailResponses, string fields, int Id, HttpContext httpContext, bool hateoss);
}

