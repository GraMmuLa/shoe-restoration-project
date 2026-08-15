using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoe_restoration_project.Models;

[Table("Shoes")]
public partial class Shoe
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("Title", TypeName = "nvarchar(64)")]
    public string Title { get; set; } = null!;

    [Required]
    [Column("Description", TypeName = "nvarchar(MAX)")]
    public string Description { get; set; } = null!;

    [Column("BrandId", TypeName = "int")]
    public int BrandId { get; set; }

    [Column("ShoeTypeId", TypeName = "int")]
    public int ShoeTypeId { get; set; }

    [Column("ColorId", TypeName = "int")]
    public int ColorId { get; set; }

    [Column("ConditionId", TypeName = "int")]
    public int ConditionId { get; set; }

    [Column("SizeId", TypeName = "int")]
    public int SizeId { get; set; }

    [Column("SkinTypeId", TypeName = "int")]
    public int SkinTypeId { get; set; }

    [Required]
    [ForeignKey("BrandId")]
    public Brand Brand { get; set; } = null!;

    [Required]
    [ForeignKey("ColorId")]
    public Color Color { get; set; } = null!;

    [Required]
    [ForeignKey("ConditionId")]
    public Condition Condition { get; set; } = null!;

    public ICollection<ShoeImage> ShoeImages { get; set; } = new List<ShoeImage>();

    public ICollection<ShoeMeasurement> ShoeMeasurements { get; set; } = new List<ShoeMeasurement>();

    [Required]
    [ForeignKey("ShoeTypeId")]
    public ShoeType ShoeType { get; set; } = null!;

    [Required]
    [ForeignKey("SizeId")]
    public Size Size { get; set; } = null!;

    [Required]
    [ForeignKey("SkinTypeId")]
    public SkinType SkinType { get; set; } = null!;
}
