using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Trackem.ERT.Api.Filters;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;


namespace Trackem.ERT.Web.Apis.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class ClaimController : ControllerBase
{

    private readonly ILogger<ClaimController> _logger;
    private readonly IServiceManager _service;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ClaimController(IServiceManager service, ILogger<ClaimController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation($"ClaimController {_service}");
    }


    [HttpGet]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> Get([FromQuery] ClaimParameter parameter)
    {
        var linkParams = new RequestParameters<ClaimParameter>(parameter, HttpContext);
        var result = await _service.ClaimService.FetchClaimsAsync(1, linkParams, false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
        return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
    }

    [HttpGet("Search")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<ResponseModel> Search([FromQuery] ClaimParameter parameter)
    {
        var linkParams = new RequestParameters<ClaimParameter>(parameter, HttpContext);
        var result = await _service.ClaimService.FetchClaimsAsync(1, linkParams, false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
        //var model = new ResponseModel { LinkedEntities = result.linkResponse.LinkedEntities, ShapedEntities = result.linkResponse.ShapedEntities };
        var model = new ResponseModel { ViewModels = result.linkResponse.ShapedEntities };
        return model;
    }

   

}
