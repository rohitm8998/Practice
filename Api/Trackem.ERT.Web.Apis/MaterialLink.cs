using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Common.Entities;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Contracts;

namespace Trackem.ERT.Web.Apis;
public class MaterialLink : IMaterialLink
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IDataShaper<MaterialGridDetailResponse> _dataShaper;

    public MaterialLink(LinkGenerator linkGenerator, IDataShaper<MaterialGridDetailResponse> dataShaper)
    {
        _linkGenerator = linkGenerator;
        _dataShaper = dataShaper;
    }
    #region Public
    public LinkResponse TryGenerateLinks(IEnumerable<MaterialGridDetailResponse> materialGridDetailResponses, string fields, int Id, HttpContext httpContext, bool hateoss)
    {
        var shapedPlants = ShapeData(materialGridDetailResponses, fields);
        return ReturnShapedPlants(shapedPlants);
    }

    #endregion

    #region Private
    private List<Entity> ShapeData(IEnumerable<MaterialGridDetailResponse> materialGridDetailResponses, string fields) =>
        _dataShaper.ShapeData(materialGridDetailResponses, fields)
            .Select(e => e.Entity)
            .ToList();

    private static LinkResponse ReturnShapedPlants(List<Entity> shapedPlants) => new() { ShapedEntities = shapedPlants };
    #endregion
}

