using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackem.ERT.Infra.EFCore.Entities;
public class Schedule
{
    [Key]
    public long SystemId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ProjectName { get; set; }

    #region Tables Relationship 
    [ForeignKey("Companies")] // Alok 
    public long CompanySystemId
    {
        get; set;
    }
    public Company Company { get; set; }
    #endregion

}