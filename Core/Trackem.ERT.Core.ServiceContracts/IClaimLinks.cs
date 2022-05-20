using Microsoft.AspNetCore.Http;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Infra.Common.Entities.LinkModels;

namespace Trackem.ERT.Core.ServiceContracts
{
    public interface IClaimLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<ClaimViewModel> claimViewModels, string fields, int companyId, HttpContext httpContext);
    }
}
