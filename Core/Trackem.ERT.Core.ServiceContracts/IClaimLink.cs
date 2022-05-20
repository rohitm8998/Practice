using Microsoft.AspNetCore.Http;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Infra.Common.Entities.LinkModels;

namespace Trackem.ERT.Core.ServiceContracts;
public interface IClaimLink
{
    LinkResponse TryGenerateLinks(IEnumerable<ClaimViewModel> claimViewModels, string fields, int Id, HttpContext httpContext, bool hateoss);
}