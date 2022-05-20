using System.ComponentModel.DataAnnotations;

namespace Trackem.ERT.Infra.EFCore.Entities;

public class Company
{
    [Key]
    public long SystemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LogoUrl { get; set; }

    //public ICollection<CompanyApiKey> CompanyApiKeys { get; set; }
    //public ICollection<Report> Reports { get; set; }
}