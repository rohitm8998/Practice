using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackem.ERT.Infra.EFCore.Entities;
public class Material
{
    [Key]
    public long SystemId { get; set; }

    public string? Barcode { get; set; }
    public string? RFIDTag { get; set; }
    public string? MaterialReferenceNumber { get; set; }
    public string? Description { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public decimal? Width { get; set; }
    public decimal? Length { get; set; }
    public decimal? Qty { get; set; }
    public decimal? POQty { get; set; }
    public string? UOM { get; set; }
    public string? UOMValue { get; set; }
    public string? ERPNumber { get; set; }
    public string? ThreeDModelId { get; set; }
    public string? Comments { get; set; }
    public string? PONumber { get; set; }

    [ForeignKey("Category")]
    public long CategorySystemId { get; set; }
    [ForeignKey("SubCategory")]
    public long SubCategorySystemId { get; set; }
    public long? LocationSystemId { get; set; }
    public long? ConditionSystemId { get; set; }
    public long? WorkorderSystemId { get; set; }
    public long? SupplierSystemId { get; set; }
    public long? StatusSystemId { get; set; }

    [ForeignKey("Schedules")] // Alok 
    public long ScheduleSystemId { get; set; }

    public long? GroupSystemId { get; set; }

    [Timestamp]
    public byte[] Version { get; set; }
    public string? Workpack { get; set; }
    public string? FabricationWorkpack { get; set; }
    public string? TestPack { get; set; }
    public string? System { get; set; }
    public string? SubSystem { get; set; }
    public string? ClientRefNumber { get; set; }
    public string? CostCode { get; set; }
    public string? DN { get; set; }
    public string? DrawingNumber { get; set; }
    public string? LineNumber { get; set; }
    public string? ISONumber { get; set; }
    public string? HeatOrBatchNumber { get; set; }
    public string? CommodityOrStockCode { get; set; }
    public string? Area { get; set; }
    public bool? FreeIssue { get; set; }
    public DateTime? RequiredOnSiteDate { get; set; }
    public DateTime? ForecastArrivalDate { get; set; }
    public DateTime? ForecastInstallationDate { get; set; }
    public string? P6ID { get; set; }
    public string? WBSNumber { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Discipline { get; set; }
    public string? Revision { get; set; }
    public bool? IsTransferred { get; set; }
    public long? CustodianSystemId { get; set; }
    public string? TransferReferenceNumber { get; set; }

    #region Tables Relationship 

    public Schedule Schedule { get; set; }
    /*
public WorkOrder Workorder { get; set; }
public Supplier Supplier { get; set; }
public Status Status { get; set; }
public Location Location { get; set; }
public Condition Condition { get; set; }
public Category Category { get; set; }
public SubCategory SubCategory { get; set; }
public Employee Custodian { get; set; }
public MaterialAdditionalColumn MaterialAdditionalColumn { get; set; }
public LogisticsDetail LogisticsDetail { get; set; }


[JsonIgnore]
public IList<ListMaterialMapping> ListMapping { get; set; }
public Group Group { get; set; }
[JsonIgnore]
public IList<OSD> OsdList { get; set; }

[JsonIgnore]
public IList<MaterialDocumentMapping> DocumentList { get; set; }
[JsonIgnore]
public List<MaterialGateTransaction> MaterialGateTransactions { get; set; }

[JsonIgnore]
public ICollection<GPSReading> GPSReadings { get; set; }
*/

    #endregion
}