using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackem.ERT.Infra.EFCore.Entities;

public class ClaimUIAccess
{
    [Column("Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "ClaimId is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Claim Value is 50 characters.")]
    [Column("ClaimId")]
    public string ClaimId { get; set; }

    [Required(ErrorMessage = "UIAccessId is a required field.")]
    [Column("UIAccessId")]
    public int UIAccessId { get; set; }
 
    //public ICollection<Claims> Claims { get; set; }
}