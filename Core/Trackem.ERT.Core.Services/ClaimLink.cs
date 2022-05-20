//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.Net.Http.Headers;
//using Trackem.ERT.Core.DataModels;
//using Trackem.ERT.Core.ServiceContracts;
//using Trackem.ERT.Infra.Common.Entities;
//using Trackem.ERT.Infra.Common.Entities.LinkModels;
//using Trackem.ERT.Infra.Contracts;

//namespace Trackem.ERT.Core.Services
//{
//    public class ClaimLinkAlok : IClaimLink
//    {
//        private readonly LinkGenerator _linkGenerator;
//        private readonly IDataShaper<ClaimViewModel> _dataShaper;

//        #region Public

//        public ClaimLinkAlok(LinkGenerator linkGenerator, IDataShaper<ClaimViewModel> dataShaper)
//        {
//            _linkGenerator = linkGenerator;
//            _dataShaper = dataShaper;
//        }

//        public LinkResponse TryGenerateLinks(IEnumerable<ClaimViewModel> claimViewModels, string fields, int id, HttpContext httpContext)
//        {
//            var shapedClaims = ShapeData(claimViewModels, fields);

//            if (ShouldGenerateLinks(httpContext))
//                return ReturnLinkdedClaims(claimViewModels, fields, id, httpContext, shapedClaims);

//            return ReturnShapedClaims(shapedClaims);
//        }
//        #endregion

//        #region Private
//        private List<Entity> ShapeData(IEnumerable<ClaimViewModel> claimsvm, string fields) =>
//            _dataShaper.ShapeData(claimsvm, fields)
//                .Select(e => e.Entity)
//                .ToList();

//        private bool ShouldGenerateLinks(HttpContext httpContext)
//        {
//            //return false; 
//            // if links needed Will remove and uncomment below
//            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

//            return mediaType != null && mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
//        }

//        private static LinkResponse ReturnShapedClaims(List<Entity> shapedClaims) => new() { ShapedEntities = shapedClaims };

//        private LinkResponse ReturnLinkdedClaims(IEnumerable<ClaimViewModel> claimViewModel, string fields, int id, HttpContext httpContext, List<Entity> shapedClaims)
//        {
//            var claimViewModelList = claimViewModel.ToList();

//            for (var index = 0; index < claimViewModelList.Count(); index++)
//            {
//                var claimLinks = CreateLinksForClaim(httpContext, id, claimViewModelList[index].Id, fields);
//                shapedClaims[index].Add("Links", claimLinks);
//            }

//            var claimCollection = new LinkCollectionWrapper<Entity>(shapedClaims);
//            var linkedClaims = CreateLinksForClaims(httpContext, claimCollection);

//            return new LinkResponse { HasLinks = true, LinkedEntities = linkedClaims };
//        }

//        private List<Link> CreateLinksForClaim(HttpContext httpContext, int claimId, int id, string fields = "")
//        {
//            id = claimId;
//            var links = new List<Link>
//            {
//                 new Link(_linkGenerator.GetUriByAction(httpContext, "GetClaim", values: new { claimId, id, fields }),
//                "self",
//                "GET"),
//                new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteClaim", values: new { claimId, id }),
//                "delete_claim",
//                "DELETE"),
//                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateClaim", values: new { claimId, id }),
//                "update_claim",
//                "PUT"),
//                new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateClaims", values: new { claimId, id }),
//                "partially_update_claim",
//                "PATCH")
//            };


//            return links;
//        }

//        private LinkCollectionWrapper<Entity> CreateLinksForClaims(HttpContext httpContext, LinkCollectionWrapper<Entity> claimsWrapper)
//        {
//            claimsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetClaimsFor", values: new { }),
//                    "self", "GET"));

//            return claimsWrapper;
//        }
//        #endregion
//    }
//}
