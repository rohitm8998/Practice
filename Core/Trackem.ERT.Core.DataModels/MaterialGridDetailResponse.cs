namespace Trackem.ERT.Core.DataModels;

public record MaterialGridDetailResponse
{
    public long SystemId { get; set; }
    public long CompanySystemId { get; set; }
    public long ScheduleSystemId { get; set; }
    public string? MaterialReferenceNumber { get; set; }
    public string? Barcode { get; set; }
    public string? RfidTag { get; set; }
    public string? Description { get; set; }
    public long CategorySystemId { get; set; }
    public string? CategoryName { get; set; }
    public long SubCategorySystemId { get; set; }
    public string? SubCategoryName { get; set; }
    public string? Comments { get; set; }
    public long? GroupSystemId { get; set; }
    public string? GroupName { get; set; }
    public string? Name { get; set; }
    public string? ScheduleName { get; set; }
    public string? ScheduleProjectName { get; set; }

    public string? CompanyName { get; set; }
}
