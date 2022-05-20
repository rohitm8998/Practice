using Microsoft.Net.Http.Headers;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Common.Entities;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Contracts;


namespace Trackem.ERT.Web.Apis;

public class ClaimLink : IClaimLink
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<ClaimViewModel> _dataShaper;

    

    public ClaimLink(LinkGenerator linkGenerator, IDataShaper<ClaimViewModel> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }
    #region Public
    public LinkResponse TryGenerateLinks(IEnumerable<ClaimViewModel> claimViewModels, string fields, int id, HttpContext httpContext, bool hateoss = false)
    {
        var shapedPlants = ShapeData(claimViewModels, fields);
        if (hateoss)
        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkdedPlants(claimViewModels, fields, id, httpContext, shapedPlants);

        return ReturnShapedPlants(shapedPlants);
    }
    #endregion

    #region Private
    private List<Entity> ShapeData(IEnumerable<ClaimViewModel> claimViewModels, string fields) =>
        _dataShaper.ShapeData(claimViewModels, fields)
            .Select(e => e.Entity)
            .ToList();

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        //return false; 
        // if links needed Will remove and uncomment below
        var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

        return mediaType != null && mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }

    private static LinkResponse ReturnShapedPlants(List<Entity> shapedPlants) => new() { ShapedEntities = shapedPlants };

    private LinkResponse ReturnLinkdedPlants(IEnumerable<ClaimViewModel> ClaimViewModel, string fields, int id, HttpContext httpContext, List<Entity> shapedPlants)
    {
        var ClaimViewModelList = ClaimViewModel.ToList();

        for (var index = 0; index < ClaimViewModelList.Count(); index++)
        {
            var plantLinks = CreateLinksForPlant(httpContext, id, ClaimViewModelList[index].Id, fields);
            shapedPlants[index].Add("Links", plantLinks);
        }

        var plantCollection = new LinkCollectionWrapper<Entity>(shapedPlants);
        var linkedPlants = CreateLinksForPlants(httpContext, plantCollection);

        return new LinkResponse { HasLinks = true, LinkedEntities = linkedPlants };
    }

    private List<Link> CreateLinksForPlant(HttpContext httpContext, int plantId, int id, string fields = "")
    {
        id = plantId;
        var links = new List<Link>
            {
                 new Link(_linkGenerator.GetUriByAction(httpContext, "GetClaim", values: new { plantId, id, fields }),
                "self",
                "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteClaim", values: new { plantId, id }),
                "delete_plant",
                "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateClaim", values: new { plantId, id }),
                "update_plant",
                "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateClaims", values: new { plantId, id }),
                "partially_update_plant",
                "PATCH")
            };


        return links;
    }

    private LinkCollectionWrapper<Entity> CreateLinksForPlants(HttpContext httpContext, LinkCollectionWrapper<Entity> plantsWrapper)
    {
        plantsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetPlantsFor", values: new { }),
                "self", "GET"));

        return plantsWrapper;
    }
    #endregion
}