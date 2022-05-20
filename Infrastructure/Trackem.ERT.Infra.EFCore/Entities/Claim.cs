using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackem.ERT.Infra.EFCore.Entities;

public class Claims
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Claim Value is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Claim Value is 50 characters.")]
    [Column("Value")]
    public string ClaimValue { get; set; }
    
    [Required(ErrorMessage = "Company address is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
    public int ClaimTypeId { get; set; }
    
}
