using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("Brands")]
public partial class Brand
{
    [Key]
    [Column("Id", TypeName = "int")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Name", TypeName = "nvarchar(128)")]
    public string Name { get; set; } = null!;

    [Column("Description", TypeName = "nvarchar(MAX)")]
    public string? Description { get; set; }

    [Column("CountryId", TypeName = "int")]
    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    public Country Country { get; set; } = null!;

    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
