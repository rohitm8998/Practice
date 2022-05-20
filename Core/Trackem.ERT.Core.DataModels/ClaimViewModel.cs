using System.ComponentModel.DataAnnotations;

namespace Trackem.ERT.Core.DataModels;

public class ClaimViewModel
{
   
    public int Id { get; set; }

    [Required(ErrorMessage = "Claim Value is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Claim Value is 50 characters.")]
    
    public string? ClaimValue { get; set; }

    [Required(ErrorMessage = "Company address is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
    public int ClaimTypeId { get; set; }
}