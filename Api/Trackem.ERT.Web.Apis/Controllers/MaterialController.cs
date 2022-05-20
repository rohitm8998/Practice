using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Trackem.ERT.Api.Filters;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;

namespace Trackem.ERT.Web.Apis.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class MaterialController : ControllerBase
{
    private readonly ILogger<MaterialController> _logger;
    private readonly IServiceManager _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Material Controller
    /// </summary>
    /// <param name="service">IServiceManager</param>
    /// <param name="logger">ILogger</param>
    /// <param name="httpContextAccessor">IHttpContextAccessor</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MaterialController(IServiceManager service, ILogger<MaterialController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation($"MaterialController {_service}");
    }

    /// <summary>
    /// This is for Materia Search V1 Search inlcude Schduled and Company Id
    /// </summary>
    /// <param name="parameter">MaterialParameter </param>
    /// <returns>LinkResponse</returns>
    [HttpGet("search")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> Get([FromQuery] MaterialParameter parameter)
    {
        var linkParams = new RequestParameters<MaterialParameter>(parameter, HttpContext);
        var result = await _service.MaterialService.FetchMaterialsAsync(1, linkParams, false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

        return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
    }
}