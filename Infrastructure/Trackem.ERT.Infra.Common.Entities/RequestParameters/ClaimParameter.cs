namespace Trackem.ERT.Infra.Common.Entities.RequestParameters;

public class ClaimParameter : BaseRequestParameters
{
    //public string? OrderBy { get; set; }
    //public string? SearchTerm { get; set; }
    #region Data Shaping
    public string? Fields { get; set; }
    #endregion
    #region Qty Range
    //public int MinId { get; set; }
    //public int MaxId { get; set; } = int.MaxValue;
    //public bool ValidQtyRange => MinId > MaxId;
    #endregion
    #region Serach fields
    //public string SPlantCodes { get; set; }
    //public string SDeliveryNum { get; set; }
    //public DateTimeOffset? SMinCreationDate { get; set; }
    //public DateTimeOffset? SMaxCreationDate { get; set; }
    //public string SMaterialNum { get; set; }
    //public string SPurchaseOrderNum { get; set; }
    //public string SCostObject { get; set; }
    //public string SWorkOrderNum { get; set; }
    //public string SRoute { get; set; }
    #endregion
}

