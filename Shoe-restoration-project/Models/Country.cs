using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("Countries")]
public partial class Country
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name", TypeName = "nvarchar(64)")]
    [MaxLength(64)]
    public string Name { get; set; } = null!;

    [Required]
    [Column("IsoCode", TypeName = "nchar(2)")]
    [MaxLength(2)]
    public string IsoCode { get; set; } = null!;

    public ICollection<Brand> Brands { get; set; } = new List<Brand>();
}
