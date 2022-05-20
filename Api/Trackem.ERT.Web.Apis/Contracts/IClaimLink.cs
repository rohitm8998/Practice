using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Infra.Common.Entities.LinkModels;

namespace Trackem.ERT.Web.Apis.Contracts;

public interface IClaimLink1
{
    LinkResponse TryGenerateLinks(IEnumerable<ClaimViewModel> claimViewModels, string fields, int Id, HttpContext httpContext);
}