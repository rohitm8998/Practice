using System.ComponentModel.DataAnnotations;

namespace Trackem.ERT.Infra.Common.Entities.RequestParameters;
public class MaterialParameter : BaseRequestParameters
{
    /// <summary>
    /// Company Id
    /// </summary>
    [Required]
    public long? CompanyId { get; set; }

    /// <summary>
    /// Scehdule Id
    /// </summary>
    public long? ScheduleId { get; set; }
    #region Data Shaping

    /// <summary>
    /// Fields choice, not mandatory. If pass blank all defined fields display.
    /// </summary>
    public string? Fields { get; set; }
    #endregion
}

