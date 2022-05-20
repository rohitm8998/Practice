using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Trackem.ERT.Api.Filters;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Core.ServiceContracts;
using Trackem.ERT.Infra.Common.Entities.LinkModels;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;

namespace Trackem.ERT.Web.Apis.Controllers
{
    /// <summary>
    /// Material Controller V2
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
      
    public class MaterialV2Controller : ControllerBase
    {
        private readonly ILogger<MaterialController> _logger;
        private readonly IServiceManager _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MaterialV2Controller(IServiceManager service, ILogger<MaterialController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"MaterialController {_service.ToString()}");
        }

        /// <summary>
        /// Materia Search V2
        /// </summary>
        /// <param name="parameter">Material Parameter</param>
        /// <returns>Result</returns>
        [HttpGet("search")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<ResponseModel> Get([FromQuery] MaterialParameter parameter)
        {
            var linkParams = new RequestParameters<MaterialParameter>(parameter, HttpContext);
            var result = await _service.MaterialService.FetchMaterialsAsync(1, linkParams, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));
            var model = new ResponseModel { ViewModels = result.linkResponse.ShapedEntities };
            return model;
            //return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
        }
    }
}
